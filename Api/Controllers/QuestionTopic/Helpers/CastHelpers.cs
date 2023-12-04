using Api.Controllers.QuestionTopic.Dto.Response;
using Dal.QuestionTopic.Models;

namespace Api.Controllers.QuestionTopic.Helpers;

public static class CastHelpers
{
    public static GetTopicResponse MapGetResponse(this QuestionTopicDal dal)
    {
        var response = new GetTopicResponse()
        {
            Id = dal.Id,
            TopicName = dal.TopicName
        };
        return response;
    }
}