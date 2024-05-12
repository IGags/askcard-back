using Core.Settings.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Core.Caching;

public static class CacheForStartup
{
    public static IServiceCollection AddCache(this IServiceCollection collection)
    {
        collection.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<CacheSettings>>();
            return ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        });

        collection.AddScoped(sp =>
        {
            var multiplexer = sp.GetRequiredService<ConnectionMultiplexer>();
            return multiplexer.GetDatabase();
        });

        return collection;
    }
}