using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.QuestionTopic.Dto.Request;

public class CreateTopicRequest
{
    [Required]
    public required string TopicName { get; init; }
}