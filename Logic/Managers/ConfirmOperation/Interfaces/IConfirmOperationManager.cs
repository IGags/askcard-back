using System;
using System.Data.Common;
using System.Threading.Tasks;
using Logic.Managers.ConfirmOperation.Models;

namespace Logic.Managers.ConfirmOperation.Interfaces;

public interface IConfirmOperationManager
{
    public Task<ConfirmOperationCreateResultModel> CreateOperationAsync<TData>(string operationName, TData customData, Guid userId,
        int? codeLength = null, int? attemptCount = null, TimeSpan? operationLifetime = null) where TData : class;

    public Task<TData> ConfirmOperationAsync<TData>(string operationId, string code, Guid userId) where TData : class;
}