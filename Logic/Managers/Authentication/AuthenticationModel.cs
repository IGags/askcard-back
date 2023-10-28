namespace Logic.Managers.Authentication;

public record AuthenticationModel
{
    public required string Email { get; init; }
    
    public required string Password { get; init; }
}