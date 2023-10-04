using System;

namespace Logic.Managers.Registration.Interfaces;

public record ConfirmUserModel
{
    public required Guid OperationId { get; init; }
    
    public required string Code { get; init; }
}