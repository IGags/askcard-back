using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Validation.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class MinValueAttribute : ValidationAttribute
{
    private readonly int _minValue;

    public MinValueAttribute(int minValue)
    {
        _minValue = minValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int integer && integer >= _minValue)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Ошибка валидации");
    }
}