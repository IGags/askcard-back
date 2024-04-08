using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Attempt.Dto.Request;

public record CreateAttemptRequest
{
    [Required]
    public required Guid UserId { get; set; }
    
    [Required]
    public required int UserScore { get; set; }
    
    [Required]
    public required int MaxPossibleScore { get; set; }
    
    [Required]
    public required TimeSpan AttemptTime { get; set; }
    
    [Required]
    public required DateTime AttemptStartTime { get; set; }
}