using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Smtp.Interfaces;
using Dal.User;
using Dal.User.Interfaces;
using Logic.Exceptions;
using Logic.Managers.ConfirmOperation.Interfaces;
using Logic.Managers.PasswordRestore.Interfaces;
using Logic.Managers.PasswordRestore.Models;
using Newtonsoft.Json.Linq;

namespace Logic.Managers.PasswordRestore;

public class RestorePasswordManager : IRestorePasswordManager
{
    private readonly ISmtpSender _smtpSender;
    private readonly IUserRepository _userRepository;
    private readonly IConfirmOperationManager _confirmOperationManager;

    public RestorePasswordManager(ISmtpSender smtpSender,
         IUserRepository userRepository, IConfirmOperationManager confirmOperationManager)
    {
        _smtpSender = smtpSender;
        _userRepository = userRepository;
        _confirmOperationManager = confirmOperationManager;
    }
    
    public async Task<string> StartRestorePassword(RestorePasswordModel model)
    {
        model.NewPassword = PasswordHashHelper.GetPasswordHash(model.NewPassword);
        var transaction = _userRepository.BeginTransaction();
        var isExists = await _userRepository.UserExistsByEmail(model.Email.Value, transaction);
        if (!isExists)
        {
            throw new UserDoesNotExistsException();
        }
        await transaction.CommitAsync();
        var codeModel = await _confirmOperationManager.CreateOperationAsync(Constants.RestorePasswordOperationName, model, Guid.Empty);
        
        await _smtpSender.SendAsync(model.Email, "Смена пароля", $"Ваш код для смены пароля {codeModel.Code}");

        return codeModel.OperationId;
    }

    public async Task CompleteRestorePassword(string operationName, string code)
    {
        var data = await _confirmOperationManager.ConfirmOperationAsync<RestorePasswordModel>(operationName, code, Guid.Empty);
        var transaction = _userRepository.BeginTransaction();
        var user = (await _userRepository.GetByFieldAsync(nameof(UserDal.Email), data.Email.Value, transaction)).Single();
        user.PasswordHash = data.NewPassword;
        await _userRepository.UpdateAsync(user, transaction);
        await transaction.CommitAsync();
    }
}