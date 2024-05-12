using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.PasswordRestore.Dto.Request;

public record StartRestorePasswordRequest
{
    [Required]
    [MinLength(8)]
    public required string NewPassword { get; init; }
    
    [EmailAddress]
    public required string Email { get; init; }
}