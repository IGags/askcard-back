using System;

namespace Api.Controllers.QuestionTopic.Dto.Response;

public class GetTopicResponse
{
    public Guid Id { get; init; }
    
    public string TopicName { get; init; }
}