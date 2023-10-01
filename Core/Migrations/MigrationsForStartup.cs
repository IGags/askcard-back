using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Migrations;

public static class MigrationsForStartup
{
    public static IServiceCollection AddMigrationRunner(this IServiceCollection collection)
    {
        collection.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=askcard-api;Pooling=true")
                .ScanIn(AppDomain.CurrentDomain.GetAssemblies()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        return collection;
    }
}