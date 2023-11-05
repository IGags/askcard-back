using System;
using System.Threading.Tasks;
using Logic.Managers.ConfirmOperation.Models;

namespace Logic.Managers.ConfirmOperation.Interfaces;

public interface IConfirmOperationManager
{
    public Task<ConfirmOperationCreateResultModel> CreateOperationAsync<TData>(ConfirmOperationModel<TData> model) where TData : class;

    public Task<TData> ConfirmOperationAsync<TData>(Guid operationId, string operationName, string code) where TData : class;
}