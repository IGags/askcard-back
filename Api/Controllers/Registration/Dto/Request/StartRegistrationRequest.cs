using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Registration.Dto.Request;

/// <summary>
/// Запрос на старт регистрации
/// </summary>
public record StartRegistrationRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; init; }
    
    [Required]
    [MinLength(5)]
    public string Login { get; init; }
    
    [Required]
    [MinLength(8)]
    public string Password { get; init; }
    
    [Required]
    public bool? IsAgree { get; init; }
}