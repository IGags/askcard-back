using System;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Repository;
using Dal.Question.Repositories.Interfaces;
using Dal.QuestionTopic.Models;
using Dal.QuestionTopic.Repositories.Interfaces;

namespace Dal.QuestionTopic.Repositories;

public class QuestionTopicRepository : Repository<QuestionTopicDal, Guid>, IQuestionTopicRepository
{
    public QuestionTopicRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}