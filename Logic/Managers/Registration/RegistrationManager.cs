using System;
using System.Threading.Tasks;
using Core.Helpers;
using Core.RepositoryBase.Connection.Interfaces;
using Core.Settings.Models;
using Core.Smtp.Interfaces;
using Dal.User;
using Dal.User.Interfaces;
using Dal.UserOperation;
using Dal.UserOperation.Interfaces;
using Logic.Exceptions;
using Logic.Managers.Registration.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Logic.Managers.Registration;

internal class RegistrationManager : IRegistrationManager
{
    private readonly IUserOperationRepository _operationRepository;
    private readonly IOptions<ConfirmationSettings> _options;
    private readonly ISmtpSender _sender;
    private readonly IUserRepository _userRepository;

    public RegistrationManager(IUserOperationRepository operationRepository,
        IOptions<ConfirmationSettings> options, ISmtpSender sender,
        IUserRepository userRepository)
    {
        _operationRepository = operationRepository;
        _options = options;
        _sender = sender;
        _userRepository = userRepository;
    }
    
    public async Task<Guid> StartRegistration(CreateUserModel model)
    {
        var transaction = _operationRepository.BeginTransaction();
        var isExist = await _userRepository.UserExistsByEmail(model.Email, transaction);
        if (isExist)
        {
            throw new UserAlreadyExistsException();
        }
        
        model = model with { Password = PasswordHashHelper.GetPasswordHash(model.Password) };
        var operationModel = new UserOperationDal()
        {
            Code = SecretCodeGenerationHelper.GenerateCode(6),
            CustomData = JObject.FromObject(model).ToString(),
            ExpirationDate = DateTime.UtcNow + TimeSpan.FromSeconds(_options.Value.ExpirationTime),
            LeftAttempts = _options.Value.AttemptCount,
            OperationName = Constants.RegistrationOperationName,
            UserId = Guid.Empty
        };
        
        var id = await _operationRepository.InsertAsync(operationModel, transaction);
        await _sender.SendAsync(model.Email, "Ваш код регистрации", operationModel.Code);

        await transaction.CommitAsync();
        
        return id;
    }

    public async Task ConfirmRegistration(ConfirmUserModel model)
    {
        var transaction = _operationRepository.BeginTransaction();
        UserOperationDal dal;
        try
        {
            dal = await _operationRepository.GetAsync(model.OperationId, transaction);
        }
        catch (Exception)
        {
            throw new OperationNotFoundException(model.OperationId);
        }

        if (!dal.OperationName.Equals(Constants.RegistrationOperationName))
        {
            await transaction.CommitAsync();
            throw new OperationNotFoundException(model.OperationId);
        }
        
        if (dal.ExpirationDate < DateTime.UtcNow)
        {
            await _operationRepository.DeleteAsync(model.OperationId, transaction);
            await transaction.CommitAsync();
            throw new OperationIsExpiredException();
        }

        if (dal.LeftAttempts <= 0)
        {
            await _operationRepository.DeleteAsync(model.OperationId, transaction);
            await transaction.CommitAsync();
            throw new IncorrectCodeException(dal.LeftAttempts);
        }
        
        if (!dal.Code.Equals(model.Code))
        {
            dal.LeftAttempts--;
            await _operationRepository.UpdateAsync(dal, transaction);
            await transaction.CommitAsync();
            throw new IncorrectCodeException(dal.LeftAttempts);
        }

        var user = JObject.Parse(dal.CustomData).ToObject<CreateUserModel>();

        var userDal = new UserDal()
        {
            Email = user.Email,
            IsAgree = user.IsAgree,
            Name = user.Login,
            PasswordHash = user.Password,
            Role = user.Role
        };

        await _userRepository.InsertAsync(userDal, transaction);
        await _operationRepository.DeleteAsync(model.OperationId, transaction);
        await transaction.CommitAsync();
    }
}