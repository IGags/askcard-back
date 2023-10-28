using System;
using Core.RepositoryBase.Repository.Interfaces;
using Dal.QuestionTopic.Models;

namespace Dal.QuestionTopic.Repositories.Interfaces;

public interface IQuestionTopicRepository : IRepository<QuestionTopicDal, Guid>
{
    
}