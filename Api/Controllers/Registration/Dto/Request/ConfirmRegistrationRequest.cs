using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Registration.Dto.Request;

public class ConfirmRegistrationRequest
{
    [Required]
    public string OperationId { get; init; }
    
    [Required]
    public string Code { get; init; }
}