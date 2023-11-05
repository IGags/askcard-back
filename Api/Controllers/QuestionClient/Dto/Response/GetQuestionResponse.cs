using System;
using Dal.Question.Models;
using Newtonsoft.Json.Linq;

namespace Api.Controllers.QuestionClient.Dto.Response;

public record GetQuestionResponse
{
    public required Guid Id { get; init; }
    
    public required QuestionType QuestionType { get; set; }
    
    public required JObject QuestionData { get; set; }
    
    public required Guid TopicId { get; set; }
}