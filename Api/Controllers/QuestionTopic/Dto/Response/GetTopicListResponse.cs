using System.Collections.Generic;

namespace Api.Controllers.QuestionTopic.Dto.Response;

public class GetTopicListResponse
{
    public List<GetTopicResponse> ResponseList { get; init; }
}