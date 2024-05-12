using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.PasswordRestore.Dto.Request;

public record ConfirmRestorePasswordRequest
{
    [Required]
    public required string OperationId { get; init; }
    
    [Required]
    public required string Code { get; init; }
}