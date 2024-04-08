using System;

namespace Api.Controllers.Attempt.Dto.Response;

public record CreateAttemptResponse
{
    public Guid Id { get; init; }
}