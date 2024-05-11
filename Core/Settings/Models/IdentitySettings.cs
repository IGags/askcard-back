using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings.Models;

public class IdentitySettings : IValidateOptions
{
    private const string SectionName = "IdentitySettings";
    private const string IssuerName = "Issuer";
    private const string AudienceName = "Audience";
    private const string SigningKeyName = "SigningKey";
    private const string LifetimeName = "TokenLifetime";

    public IdentitySettings(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        
        Issuer = section.GetSection(IssuerName).Value;
        Audience = section.GetSection(AudienceName).Value;
        SigningKey = section.GetSection(SigningKeyName).Value;
        var isParsed = int.TryParse(section.GetSection(LifetimeName).Value, out var result);
        TokenLifetime = isParsed ? result : -1;
    }
    
    public string Issuer { get; }
    
    public string Audience { get; }
    
    public string SigningKey { get; }
    
    public int TokenLifetime { get; }
    
    public void Validate()
    {
        var errorList = new List<string>();
        if (string.IsNullOrWhiteSpace(Issuer))
        {
            errorList.Add("Не указан издатель токена");
        }
        if (string.IsNullOrWhiteSpace(Audience))
        {
            errorList.Add("Не указан издатель токена");
        }
        if (string.IsNullOrWhiteSpace(SigningKey))
        {
            errorList.Add("Не указан ключ подписи токена");
        }
        if (TokenLifetime <= 0)
        {
            errorList.Add("Не валидное время жизни токена");
        }

        if (errorList.Any())
        {
            throw new OptionsValidationException(nameof(IdentitySettings), typeof(IdentitySettings), errorList);
        }
    }
}