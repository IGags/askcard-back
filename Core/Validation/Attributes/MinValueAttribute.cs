using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Validation.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
public class MinValueAttribute : ValidationAttribute
{
    private readonly int _minValue;

    public MinValueAttribute(int minValue) : base("Ошибка валидации")
    {
        _minValue = minValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int integer && integer >= _minValue)
        {
            return ValidationResult.Success;
        }
        
        return new ValidationResult("Ошибка валидации", new[] { validationContext.MemberName });
    }
}