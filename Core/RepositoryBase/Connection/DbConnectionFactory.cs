using System.Data.Common;
using Core.RepositoryBase.Connection.Interfaces;
using Core.Settings.Models;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Core.RepositoryBase.Connection;

/// <inheritdoc cref="IDbConnectionFactory"/>
public class DbConnectionFactory : IDbConnectionFactory
{
    private DbConnection _connection;
    
    private DbSettings _dbSettings;
    
    public DbConnectionFactory(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }
    
    /// <inheritdoc />
    public DbConnection GetConnection()
    {
        if (_connection != null)
        {
            return _connection;
        }

        _connection = new NpgsqlConnection(_dbSettings.ConnectionString);
        _connection.Open();
        
        return _connection;
    }

    /// <inheritdoc />
    public DbTransaction StartTransaction()
    {
        if (_connection == null)
        {
            GetConnection();
        }

        return _connection.BeginTransaction();
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}