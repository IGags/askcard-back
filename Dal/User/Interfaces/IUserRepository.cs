using System;
using System.Data;
using System.Threading.Tasks;
using Core.RepositoryBase.Repository.Interfaces;

namespace Dal.User.Interfaces;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository : IRepository<UserDal, Guid>
{
    public Task<bool> UserExistsByEmail(string email, IDbTransaction transaction);
}