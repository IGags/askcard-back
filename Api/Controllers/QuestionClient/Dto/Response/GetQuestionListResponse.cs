using System.Collections.Generic;

namespace Api.Controllers.QuestionClient.Dto.Response;

public record GetQuestionListResponse
{
    public required List<GetQuestionResponse> QuestionList { get; init; }
}