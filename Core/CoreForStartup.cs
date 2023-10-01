using Core.Migrations;
using Core.RepositoryBase.Connection;
using Core.RepositoryBase.Connection.Interfaces;
using Core.Settings;
using Core.Smtp;
using Core.Smtp.Interfaces;
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
        collection.AddSettings();

        collection.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        collection.AddTransient<ISmtpSender, SmtpSender>();
        collection.AddMigrationRunner();
        
        return collection;
    }
}