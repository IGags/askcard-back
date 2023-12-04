using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Core.RepositoryBase;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Repository;
using Dal.Question.Models;
using Dal.Question.Repositories.Interfaces;
using Dapper;

namespace Dal.Question.Repositories;

public class QuestionRepository : Repository<QuestionDal, Guid>, IQuestionRepository
{
    public QuestionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<List<QuestionDal>> GetRandomQuestionListAsync(int count, Guid topicId, DbTransaction transaction)
    {
        var sql = $"SELECT * FROM {DalHelper.TbName<QuestionDal>()} " +
                  $"WHERE {DalHelper.ColName<QuestionDal>(x => x.TopicId)}={DalHelper.ParameterPrefix}{nameof(topicId)} " +
                  $"ORDER BY RANDOM() LIMIT " +
                  $"{DalHelper.ParameterPrefix}{nameof(count)}";
        var result = await Connection.QueryAsync<QuestionDal>(sql, new{ topicId, count }, transaction);
        return result.AsList();
    }
}