using System;
using System.Data.Common;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Settings.Models;
using Logic.Exceptions;
using Logic.Managers.ConfirmOperation.Interfaces;
using Logic.Managers.ConfirmOperation.Models;
using Logic.Managers.Registration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace Logic.Managers.ConfirmOperation;

public class ConfirmOperationManager : IConfirmOperationManager
{
    private readonly IDatabase _database;
    private readonly IOptions<ConfirmationSettings> _options;

    public ConfirmOperationManager(IDatabase database, IOptions<ConfirmationSettings> options)
    {
        _database = database;
        _options = options;
    }
    
    public async Task<ConfirmOperationCreateResultModel> CreateOperationAsync<TData>(string operationName, TData customData, Guid userId,
        int? codeLength = null, int? attemptCount = null, TimeSpan? operationLifetime = null) where TData : class
    {
        var code = SecretCodeGenerationHelper.GenerateCode(codeLength ?? 6);
        var model = new ConfirmOperationModel<TData>()
        {
            AttemptCount = attemptCount ?? _options.Value.AttemptCount,
            Code = code,
            CustomData = customData,
            UserId = userId
        };
        
        var id = $"{operationName}-{Guid.NewGuid()}";
        await _database.StringSetAsync(new RedisKey(id), new RedisValue(JsonConvert.SerializeObject(model)),
            operationLifetime ?? TimeSpan.FromSeconds(_options.Value.ExpirationTime));
        var response = new ConfirmOperationCreateResultModel()
        {
            Code = code,
            OperationId = id
        };

        return response;
    }

    public async Task<TData> ConfirmOperationAsync<TData>(string operationId, string code, Guid userId) where TData : class
    {
        var modelString = _database.StringGet(operationId);
        if (modelString.IsNullOrEmpty)
        {
            throw new OperationNotFoundException(operationId);
        }

        var operation = JsonConvert.DeserializeObject<ConfirmOperationModel<TData>>(modelString);

        if (operation.AttemptCount <= 0)
        {
            await _database.KeyDeleteAsync(operationId);
            throw new IncorrectCodeException(operation.AttemptCount);
        }

        if (operation.UserId != userId)
        {
            throw new OperationNotFoundException(operationId);
        }
        
        if (!operation.Code.Equals(code))
        {
            operation.AttemptCount--;
            await _database.StringSetAsync(new RedisKey(operationId),
                new RedisValue(JsonConvert.SerializeObject(operation)), keepTtl: true);
            throw new IncorrectCodeException(operation.AttemptCount);
        }

        var result = operation.CustomData;

        await _database.KeyDeleteAsync(operationId);

        return result;
    }
}