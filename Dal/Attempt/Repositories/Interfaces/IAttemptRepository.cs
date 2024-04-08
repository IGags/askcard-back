using System;
using Core.RepositoryBase.Repository.Interfaces;
using Dal.Attempt.Models;

namespace Dal.Attempt.Repositories.Interfaces;

public interface IAttemptRepository : IRepository<AttemptDal, Guid>
{
    
}