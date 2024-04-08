using System.Collections.Generic;

namespace Api.Controllers.Attempt.Dto.Response;

public record GetAttemptListResponse
{
    public required List<GetAttemptResponse> AttemptList { get; init; } = new();
}