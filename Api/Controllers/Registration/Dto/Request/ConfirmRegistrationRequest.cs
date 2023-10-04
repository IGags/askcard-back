using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Registration.Dto.Request;

public class ConfirmRegistrationRequest
{
    [Required]
    public Guid? OperationId { get; init; }
    
    [Required]
    public string Code { get; init; }
}