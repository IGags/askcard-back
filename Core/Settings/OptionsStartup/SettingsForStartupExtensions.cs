using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Settings.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Core.Settings.OptionsStartup;

public static class SettingsForStartupExtensions
{
    public static IServiceCollection AddSettings<T>(this IServiceCollection collection) where T : class, IValidateOptions<T>
    {
        collection.TryAddSingleton<IOptions<T>>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var ctor = (T)Activator.CreateInstance(typeof(T), configuration);
            var wrapper = new OptionsWrapper<T>(ctor);
            return wrapper;
        });

        return collection;
    }
}