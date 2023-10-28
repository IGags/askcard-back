using System.Collections.Generic;

namespace Api.Controllers.Question.Dto.Response;

public record GetQuestionListResponse
{
    public required List<GetQuestionResponse> QuestionList { get; init; }
}