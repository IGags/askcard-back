using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Authentication.Dto;

public class AuthenticationRequest
{
    [EmailAddress]
    public required string Email { get; init; }
    
    [Required]
    public required string Password { get; init; }
}