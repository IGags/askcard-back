using System;

namespace Logic.Managers.Registration.Interfaces;

public record ConfirmUserModel
{
    public required string OperationId { get; init; }
    
    public required string Code { get; init; }
}