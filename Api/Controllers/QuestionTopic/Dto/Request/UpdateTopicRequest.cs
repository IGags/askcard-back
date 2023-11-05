using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.QuestionTopic.Dto.Request;

public class UpdateTopicRequest
{
    [Required]
    public required Guid? Id { get; init; }
    
    [Required]
    public required string TopicName { get; init; }
}