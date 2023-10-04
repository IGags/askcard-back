using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings.Models;

public class ConfirmationSettings : IValidateOptions<ConfirmationSettings>
{
    private const string SectionName = "ConfirmationSettings";

    private const string ExpirationTimeName = "OperationExpirationTime";

    private const string AttemptCountName = "AttemptCount";
    
    public ConfirmationSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        var isParsed = int.TryParse(section.GetSection(ExpirationTimeName).Value, out var expirationResult);
        ExpirationTime = isParsed ? expirationResult : int.MinValue;
        isParsed = int.TryParse(section.GetSection(AttemptCountName).Value, out var result);
        AttemptCount = isParsed ? result : int.MinValue;
    }
    
    public int ExpirationTime { get; }
    
    public int AttemptCount { get; }
    
    public ValidateOptionsResult Validate(string name, ConfirmationSettings options)
    {
        var failureMessages = new List<string>();
        if (ExpirationTime == int.MinValue)
        {
            failureMessages.Add("Не указано время истечения операции");
        }

        if (AttemptCount == int.MinValue)
        {
            failureMessages.Add("Не указано количество попыток");
        }

        if (failureMessages.Any())
        {
            throw new OptionsValidationException(name, typeof(ConfirmationSettings), failureMessages);
        }

        return new ValidateOptionsResult();
    }
}