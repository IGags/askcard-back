using System.Threading.Tasks;
using Logic.Managers.ConfirmOperation.Models;

namespace Logic.Managers.ConfirmOperation.Interfaces;

public interface IConfirmOperationManager
{
    public Task<ConfirmOperationCreateResultModel> CreateOperationAsync<TData>(ConfirmOperationModel<TData> model) where TData : class, new();
}