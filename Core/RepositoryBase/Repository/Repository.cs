using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Core.RepositoryBase.Connection.Interfaces;
using Core.RepositoryBase.Model;
using Core.RepositoryBase.Repository.Interfaces;
using Dapper;

namespace Core.RepositoryBase.Repository;

/// <inheritdoc cref="IRepository{TDal,TKey}"/>
public class Repository<TDal, TKey> : IRepository<TDal, TKey> where TDal : DalModelBase<TKey>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbConnection Connection => _connectionFactory.GetConnection();
    
    public Repository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    /// <inheritdoc />
    public async Task<TKey> InsertAsync(TDal model, DbTransaction transaction)
    {
        var properties = DalHelper.GetNonIdProperties(model.GetType());
        var quotedProperties = properties.Select(x => $"\"{x}\"");
        var escapedProperties = properties.Select(x => $"{DalHelper.ParameterPrefix}{x}");
        
        var statement = $"INSERT INTO {DalHelper.TbName<TDal>()}( {string.Join(", ", quotedProperties)} ) " +
                        $"VALUES ( {string.Join(", ", escapedProperties)} ) " +
                        $"RETURNING {DalHelper.ColName<TDal>(x => x.Id)}";
        var id = await Connection.QuerySingleAsync<TKey>(statement, model, transaction);

        return id;
    }

    /// <inheritdoc />
    public async Task<TDal> GetAsync(TKey id, DbTransaction transaction)
    {
        var statement =
            $"SELECT * FROM {DalHelper.TbName<TDal>()} " +
            $"WHERE {DalHelper.ColName<TDal>(x => x.Id)} = {DalHelper.ParameterPrefix}{{nameof(id)}}";
        var result = await Connection.QuerySingleAsync<TDal>(statement, id, transaction);
        
        return result;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TKey id, DbTransaction transaction)
    {
        var statement =
            $"DELETE FROM {DalHelper.TbName<TDal>()} " +
            $"WHERE {DalHelper.ColName<TDal>(x => x.Id)} = {DalHelper.ParameterPrefix}{nameof(id)}";
        await Connection.ExecuteAsync(statement, id, transaction);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TDal newModel, DbTransaction transaction)
    {
        var statement = $"UPDATE {DalHelper.TbName<TDal>()} SET {DalHelper.GetFieldPart(typeof(TDal))} " +
                        $"WHERE {DalHelper.ColName<TDal>(x => x.Id, false)} = {DalHelper.ParameterPrefix}Id";

        await Connection.ExecuteAsync(statement, newModel, transaction);
    }

    /// <inheritdoc />
    public Task<List<TDal>> GetAllAsync(DbTransaction transaction)
    {
        throw new System.NotImplementedException();
    }
}