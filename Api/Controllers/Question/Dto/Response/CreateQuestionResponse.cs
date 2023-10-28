using System;

namespace Api.Controllers.Question.Dto.Response;

public record CreateQuestionResponse
{
    public required Guid QuestionId { get; init; }
}