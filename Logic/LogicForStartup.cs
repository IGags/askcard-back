using Logic.Managers.Authentication;
using Logic.Managers.Authentication.Interfaces;
using Logic.Managers.ConfirmOperation;
using Logic.Managers.ConfirmOperation.Interfaces;
using Logic.Managers.PasswordRestore;
using Logic.Managers.PasswordRestore.Interfaces;
using Logic.Managers.Question;
using Logic.Managers.Question.Interfaces;
using Logic.Managers.Registration;
using Logic.Managers.Registration.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class LogicForStartup
{
    public static IServiceCollection AddLogic(this IServiceCollection collection)
    {
        collection.AddTransient<IRegistrationManager, RegistrationManager>();
        collection.AddTransient<IAuthenticationManager, AuthenticationManager>();
        collection.AddTransient<IRestorePasswordManager, RestorePasswordManager>();
        collection.AddTransient<IConfirmOperationManager, ConfirmOperationManager>();
        collection.AddTransient<IQuestionManager, QuestionManager>();
        
        return collection;
    }
}