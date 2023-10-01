using System;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Repository;
using Dal.User.Interfaces;

namespace Dal.User;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public class UserRepository : Repository<UserDal, Guid>, IUserRepository
{
    public UserRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}