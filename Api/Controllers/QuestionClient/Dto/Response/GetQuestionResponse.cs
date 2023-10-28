using System;
using Dal.Question.Models;

namespace Api.Controllers.QuestionClient.Dto.Response;

public record GetQuestionResponse
{
    public required Guid Id { get; init; }
    
    public required string QuestionText { get; init; }

    public required QuestionType QuestionType { get; init; }
    
    public required string Answers { get; init; }
    
    public required Guid TopicId { get; init; }
}