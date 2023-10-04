using Dal.User;
using Dal.User.Interfaces;
using Dal.UserOperation;
using Dal.UserOperation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

/// <summary>
/// Дал слой
/// </summary>
public static class DalForStartup
{
    public static IServiceCollection AddDal(this IServiceCollection collection)
    {
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IUserOperationRepository, UserOperationRepository>();

        return collection;
    }
}