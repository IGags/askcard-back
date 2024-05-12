namespace Api.Controllers.PasswordRestore.Dto.Response;

public record StartRestorePasswordResponse
{
    public required string OperationName { get; init; }
}