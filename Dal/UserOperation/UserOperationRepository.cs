using System;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Repository;
using Dal.UserOperation.Interfaces;

namespace Dal.UserOperation;

public class UserOperationRepository : Repository<UserOperationDal, Guid>, IUserOperationRepository
{
    public UserOperationRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}