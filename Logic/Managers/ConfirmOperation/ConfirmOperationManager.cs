using System;
using System.Data.Common;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Settings.Models;
using Dal.User;
using Dal.UserOperation;
using Dal.UserOperation.Interfaces;
using Logic.Exceptions;
using Logic.Managers.ConfirmOperation.Interfaces;
using Logic.Managers.ConfirmOperation.Models;
using Logic.Managers.Registration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Logic.Managers.ConfirmOperation;

public class ConfirmOperationManager : IConfirmOperationManager
{
    private readonly IUserOperationRepository _operationRepository;
    private readonly IOptions<ConfirmationSettings> _options;

    public ConfirmOperationManager(IUserOperationRepository operationRepository, IOptions<ConfirmationSettings> options)
    {
        _operationRepository = operationRepository;
        _options = options;
    }
    
    public async Task<ConfirmOperationCreateResultModel> CreateOperationAsync<TData>(ConfirmOperationModel<TData> model, DbTransaction transaction = null) where TData : class
    {
        var code = SecretCodeGenerationHelper.GenerateCode(model.CodeLength ?? 6);
        
        var operationModel = new UserOperationDal()
        {
            Code = code,
            CustomData = JObject.FromObject(model.CustomData),
            ExpirationDate = DateTime.UtcNow 
                             + (model.OperationLifetime ?? TimeSpan.FromSeconds(_options.Value.ExpirationTime)),
            LeftAttempts = model.AttemptCount ?? _options.Value.AttemptCount,
            OperationName = model.OperationName,
            UserId = model.UserId
        };
        
        var id = await _operationRepository.InsertAsync(operationModel, transaction);

        var response = new ConfirmOperationCreateResultModel()
        {
            Code = code,
            OperationId = id
        };

        return response;
    }

    public async Task<TData> ConfirmOperationAsync<TData>(Guid operationId, string operationName, string code, DbTransaction transaction = null) where TData : class
    {
        UserOperationDal dal;
        try
        {
            dal = await _operationRepository.GetAsync(operationId, transaction);
        }
        catch (Exception)
        {
            throw new OperationNotFoundException(operationId);
        }

        if (!dal.OperationName.Equals(Constants.RegistrationOperationName))
        {
            await transaction.CommitAsync();
            throw new OperationNotFoundException(operationId);
        }
        
        if (dal.ExpirationDate < DateTime.UtcNow)
        {
            await _operationRepository.DeleteAsync(operationId, transaction);
            await transaction.CommitAsync();
            throw new OperationIsExpiredException();
        }

        if (dal.LeftAttempts <= 0)
        {
            await _operationRepository.DeleteAsync(operationId, transaction);
            await transaction.CommitAsync();
            throw new IncorrectCodeException(dal.LeftAttempts);
        }
        
        if (!dal.Code.Equals(code))
        {
            dal.LeftAttempts--;
            await _operationRepository.UpdateAsync(dal, transaction);
            await transaction.CommitAsync();
            throw new IncorrectCodeException(dal.LeftAttempts);
        }

        var result = dal.CustomData.ToObject<TData>();

        await _operationRepository.DeleteAsync(operationId, transaction);

        return result;
    }
}