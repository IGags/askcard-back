using System;
using Core.RepositoryBase.Repository.Interfaces;

namespace Dal.User.Interfaces;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository : IRepository<UserDal, Guid>
{
    
}