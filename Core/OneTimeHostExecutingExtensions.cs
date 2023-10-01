using System;
using System.Threading.Tasks;
using Core.DatabaseInitialization;
using Core.RepositoryBase.Connection.Interfaces;
using Core.Settings.Models;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Core;

public static class OneTimeHostExecutingExtensions
{
    /// <summary>
    /// Запустить логику, которую требуется выполнить 1 раз
    /// </summary>
    public static async Task RunOneTimeLogicAsync(this IHost host)
    {
        await host.InitializeDatabaseAsync();
    }
}