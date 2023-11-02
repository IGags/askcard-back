using System.Threading.Tasks;
using Dal.UserOperation.Interfaces;
using Logic.Managers.ConfirmOperation.Interfaces;
using Logic.Managers.ConfirmOperation.Models;
using Microsoft.Extensions.Options;

namespace Logic.Managers.ConfirmOperation;

public class ConfirmOperationManager : IConfirmOperationManager
{
    public ConfirmOperationManager(IUserOperationRepository userOperationRepository, IOptions<Conf>)
    {
        
    }
    
    public Task<ConfirmOperationCreateResultModel> CreateOperationAsync<TData>(ConfirmOperationModel<TData> model) where TData : class, new()
    {
        
    }
}