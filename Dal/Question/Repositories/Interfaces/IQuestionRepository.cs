using System;
using Core.RepositoryBase.Repository.Interfaces;
using Dal.Question.Models;

namespace Dal.Question.Repositories.Interfaces;

public interface IQuestionRepository : IRepository<QuestionDal, Guid> { }