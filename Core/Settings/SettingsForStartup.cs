using Core.Settings.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Core.Settings;

public static class SettingsForStartup
{
    public static IServiceCollection AddSettings(this IServiceCollection collection)
    {
        collection.TryAddSingleton<IOptions<DbSettings>>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            return new OptionsWrapper<DbSettings>(new DbSettings(configuration));
        });
        
        collection.TryAddSingleton<IOptions<DebugSettings>>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            return new OptionsWrapper<DebugSettings>(new DebugSettings(configuration));
        });
        
        collection.TryAddSingleton<IOptions<SmtpSettings>>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new OptionsWrapper<SmtpSettings>(new SmtpSettings(configuration));
        });

        return collection;
    }
}