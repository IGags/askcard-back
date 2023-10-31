using System;
using System.ComponentModel.DataAnnotations;
using Dal.Question.Models;
using Newtonsoft.Json.Linq;

namespace Api.Controllers.Question.Dto.Response;

public record GetQuestionResponse
{
    public required Guid Id { get; init; }
    
    public required string QuestionText { get; init; }

    public required QuestionType QuestionType { get; init; }
    
    public required JObject QuestionData { get; init; }
    
    public required Guid TopicId { get; init; }
}