using System;
using Core.RepositoryBase.Model;
using Newtonsoft.Json.Linq;

namespace Dal.Question.Models;

public class QuestionDal : DalModelBase<Guid>
{
    public required QuestionType QuestionType { get; set; }
    
    public required JObject QuestionData { get; set; }
    
    public required Guid TopicId { get; set; }
}