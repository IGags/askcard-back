using System;

namespace Api.Controllers.Attempt.Dto.Response;

public record GetAttemptResponse
{
    public required Guid Id { get; init; }
    
    public required Guid UserId { get; init; }
    
    public required int UserScore { get; init; }
    
    public required int MaxPossibleScore { get; init; }
    
    public required TimeSpan AttemptTime { get; init; }
    
    public required DateTime AttemptStartTime { get; init; }
}