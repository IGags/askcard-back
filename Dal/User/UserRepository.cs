using System;
using System.Data;
using System.Threading.Tasks;
using Core.RepositoryBase;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Repository;
using Dal.User.Interfaces;
using Dapper;

namespace Dal.User;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public class UserRepository : Repository<UserDal, Guid>, IUserRepository
{
    public UserRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    /// <inheritdoc />
    public async Task<bool> UserExistsByEmail(string email, IDbTransaction transaction)
    {
        var sql =
            $"SELECT count(1) FROM {DalHelper.TbName<UserDal>()} " +
            $"WHERE {DalHelper.ColName<UserDal>(x => x.Email)} = {DalHelper.ParameterPrefix}{nameof(email)}";
        var count = await Connection.ExecuteScalarAsync<int>(sql, new { email }, transaction);
        return count != 0;
    }
}