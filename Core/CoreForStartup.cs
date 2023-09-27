using Microsoft.Extensions.DependencyInjection;

namespace Core;

/// <summary>
/// Добавление кора
/// </summary>
public static class CoreForStartup
{
    public static IServiceCollection AddCore(this IServiceCollection collection)
    {
        collection.AddControllers();
        collection.AddSwaggerGen();

        return collection;
    }
}