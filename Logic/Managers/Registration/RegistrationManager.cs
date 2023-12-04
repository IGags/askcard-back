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
using Logic.Managers.ConfirmOperation.Interfaces;
using Logic.Managers.ConfirmOperation.Models;
using Logic.Managers.Registration.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Logic.Managers.Registration;

internal class RegistrationManager : IRegistrationManager
{
    private readonly ISmtpSender _sender;
    private readonly IUserRepository _userRepository;
    private readonly IConfirmOperationManager _confirmOperationManager;

    public RegistrationManager(ISmtpSender sender,
        IUserRepository userRepository, IConfirmOperationManager confirmOperationManager)
    {
        _sender = sender;
        _userRepository = userRepository;
        _confirmOperationManager = confirmOperationManager;
    }
    
    public async Task<Guid> StartRegistration(CreateUserModel model)
    {
        var transaction = _userRepository.BeginTransaction();
        var isExist = await _userRepository.UserExistsByEmail(model.Email, transaction);
        if (isExist)
        {
            throw new UserAlreadyExistsException();
        }
        
        model = model with { Password = PasswordHashHelper.GetPasswordHash(model.Password) };

        var confirmModel = new ConfirmOperationModel<CreateUserModel>()
        {
            CustomData = model,
            OperationName = Constants.RegistrationOperationName,
            UserId = Guid.Empty
        };

        var resultModel = await _confirmOperationManager.CreateOperationAsync(confirmModel);
        
        await _sender.SendAsync(model.Email, "Ваш код регистрации", resultModel.Code);
       
        await transaction.CommitAsync();
        
        return resultModel.OperationId;
    }

    public async Task ConfirmRegistration(ConfirmUserModel model)
    {
        var userModel = await _confirmOperationManager.ConfirmOperationAsync<CreateUserModel>(model.OperationId,
            Constants.RegistrationOperationName, model.Code);

        var userDal = new UserDal()
        {
            Email = userModel.Email,
            IsAgree = userModel.IsAgree,
            Name = userModel.Login,
            PasswordHash = userModel.Password,
            Role = userModel.Role
        };

        var transaction = _userRepository.BeginTransaction();
        
        await _userRepository.InsertAsync(userDal, transaction);
        await transaction.CommitAsync();
    }
}