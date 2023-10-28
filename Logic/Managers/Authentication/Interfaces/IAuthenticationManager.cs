using System.Threading.Tasks;

namespace Logic.Managers.Authentication.Interfaces;

public interface IAuthenticationManager
{
    public Task<string> AuthAsync(AuthenticationModel model);
}