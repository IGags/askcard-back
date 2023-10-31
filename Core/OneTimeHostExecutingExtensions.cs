using System.Threading.Tasks;
using Core.DatabaseInitialization;
using Core.DbTypeHandlers;
using Microsoft.Extensions.Hosting;

namespace Core;

public static class OneTimeHostExecutingExtensions
{
    /// <summary>
    /// Запустить логику, которую требуется выполнить 1 раз
    /// </summary>
    public static async Task RunOneTimeLogicAsync(this IHost host)
    {
        await host.InitializeDatabaseAsync();
        TypeMappersForStartup.AddTypeMappers();
    }
}