using System;
using System.ComponentModel.DataAnnotations;
using Dal.Question.Models;

namespace Api.Controllers.Question.Dto.Request;

public record UpdateQuestionRequest
{
    [Required]
    public required Guid? Id { get; init; }
    
    [Required]
    public required string QuestionText { get; init; }

    [Required]
    public required QuestionType? QuestionType { get; init; }
    
    [Required]
    public required string Answers { get; init; }
    
    [Required]
    public required Guid? TopicId { get; init; }
}