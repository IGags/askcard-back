using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Validation.Attributes;

public class IsNotEmptyGuidAttribute : ValidationAttribute
{
    public IsNotEmptyGuidAttribute() : base("Невалидный uuid")
    {
        
    }
    
    public override bool IsValid(object value)
    {
        return value is Guid guid && guid != Guid.Empty;
    }
}