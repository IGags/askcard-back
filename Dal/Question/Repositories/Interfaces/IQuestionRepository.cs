using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Core.RepositoryBase.Repository.Interfaces;
using Dal.Question.Models;

namespace Dal.Question.Repositories.Interfaces;

public interface IQuestionRepository : IRepository<QuestionDal, Guid>
{
    Task<List<QuestionDal>> GetRandomQuestionListAsync(int count, DbTransaction transaction);
}