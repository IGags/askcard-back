using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings.Models;

public class CacheSettings : IValidateOptions
{
    private const string SectionName = "CacheSettings";

    private const string ConnectionStringName = "ConnectionString";
    
    public string ConnectionString { get; }

    public CacheSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        ConnectionString = section.GetValue<string>(ConnectionStringName);
    }
    
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            throw new OptionsValidationException(nameof(CacheSettings), typeof(CacheSettings),
                new List<string>() { "Строка подключения кэша обязательна" });
        }
    }
}