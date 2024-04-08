using System;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Repository;
using Dal.Attempt.Models;
using Dal.Attempt.Repositories.Interfaces;

namespace Dal.Attempt.Repositories;

public class AttemptRepository : Repository<AttemptDal, Guid>, IAttemptRepository
{
    public AttemptRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}