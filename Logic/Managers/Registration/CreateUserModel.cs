namespace Logic.Managers.Registration;

/// <summary>
/// Модель создания пользователя
/// </summary>
public record CreateUserModel
{
    public required string Email { get; init; }
    
    public required string Login { get; init; }
    
    public required string Password { get; init; }
    
    public required bool IsAgree { get; init; }
}