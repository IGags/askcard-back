using System;
using Core.RepositoryBase.Model;

namespace Dal.Question.Models;

public class QuestionDal : DalModelBase<Guid>
{
    public required string QuestionText { get; set; }

    public required QuestionType QuestionType { get; set; }
    
    public required string Answers { get; set; }
    
    public required Guid TopicId { get; set; }
}