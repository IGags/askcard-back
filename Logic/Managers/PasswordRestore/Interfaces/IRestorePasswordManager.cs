using System.Threading.Tasks;
using Logic.Managers.PasswordRestore.Models;

namespace Logic.Managers.PasswordRestore.Interfaces;

public interface IRestorePasswordManager
{
    public Task<string> StartRestorePassword(RestorePasswordModel model);

    public Task CompleteRestorePassword(string operationName, string code);
}