using System;
using System.ComponentModel.DataAnnotations;
using Dal.Question.Models;
using Newtonsoft.Json.Linq;

namespace Api.Controllers.Question.Dto.Request;

public record CreateQuestionRequest
{
    [Required]
    public required string QuestionText { get; init; }

    [Required]
    public required QuestionType? QuestionType { get; init; }
    
    [Required]
    public required JObject QuestionData { get; init; }
    
    [Required]
    public required Guid? TopicId { get; init; }
}