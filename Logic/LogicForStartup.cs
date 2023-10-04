using Logic.Managers.Registration;
using Logic.Managers.Registration.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class LogicForStartup
{
    public static IServiceCollection AddLogic(this IServiceCollection collection)
    {
        collection.AddTransient<IRegistrationManager, RegistrationManager>();

        return collection;
    }
}