using System;
using System.Threading.Tasks;

namespace Logic.Managers.Registration.Interfaces;

public interface IRegistrationManager
{
    public Task<string> StartRegistration(CreateUserModel model);
    
    public Task ConfirmRegistration(ConfirmUserModel model);
}