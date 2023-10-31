using System.Threading.Tasks;
using Core.Helpers;
using Core.Smtp.Interfaces;
using Dal.User.Interfaces;
using Dal.UserOperation.Interfaces;
using Logic.Managers.PasswordRestore.Interfaces;
using Logic.Managers.PasswordRestore.Models;
using Newtonsoft.Json.Linq;

namespace Logic.Managers.PasswordRestore;

public class RestorePasswordManager : IRestorePasswordManager
{
    private readonly ISmtpSender _smtpSender;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserRepository _userRepository;

    public RestorePasswordManager(ISmtpSender smtpSender, IUserOperationRepository userOperationRepository,
         IUserRepository userRepository)
    {
        _smtpSender = smtpSender;
        _userOperationRepository = userOperationRepository;
        _userRepository = userRepository;
    }
    
    public async Task StartRestorePassword(RestorePasswordModel model)
    {
        var transaction = _userOperationRepository.BeginTransaction();
        var user = await _userRepository.GetAsync(model.UserId, transaction);
        
        var code = SecretCodeGenerationHelper.GenerateCode(6);
        await _smtpSender.SendAsync(user.Email, "Смена пароля", $"Ваш новый пароль {code}");

        model.Code = code;

        var json = JObject.FromObject(model);
    }
}