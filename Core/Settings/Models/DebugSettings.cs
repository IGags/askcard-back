﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings.Models;

/// <summary>
/// Настройки дебаг мода(для деплоя)
/// </summary>
public class DebugSettings : IValidateOptions<DebugSettings>
{
    private const string KeyName = "IsDebug";
    
    /// <summary>
    /// Дебаг
    /// </summary>
    public bool? IsDebug { get; }

    public DebugSettings(IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection(KeyName).Value, out var autoCreate);
        IsDebug = autoCreate;
    }

    public ValidateOptionsResult Validate(string name, DebugSettings options)
    {
        return new ValidateOptionsResult();
    }
}