using System.Data;
using System.Data.Common;
using HR.Core.Entities;
using HR.Core.Interfaces;
using HR.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;

namespace HR.Infrastructure.Data;

/// <summary>
/// Implementation of the Unit of Work pattern
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly IDatabaseConnection _connection;
    private IDbConnection? _dbConnection;
    private IDbTransaction? _transaction;
    private bool _disposed;

    // Dictionary to store repositories
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(IDatabaseConnection connection)
    {
        _connection = connection;
    }

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        // Check if repository exists, if not create it
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<T>(_connection);
        }

        return (IRepository<T>)_repositories[type];
    }

    public async Task BeginTransactionAsync()
    {
        _dbConnection = _connection.CreateConnection();
        if (_dbConnection is DbConnection dbConnection)
        {
            await dbConnection.OpenAsync();
        }
        else
        {
            _dbConnection.Open();
        }
        _transaction = _dbConnection.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;

            await CloseConnectionAsync();
        }
    }

    public async Task RollbackAsync()
    {
        try
        {
            _transaction?.Rollback();
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;

            await CloseConnectionAsync();
        }
    }

    public Task<int> SaveChangesAsync()
    {
        // In Dapper, changes are saved immediately with each operation
        // This method is included for compatibility with the IUnitOfWork interface
        return Task.FromResult(0);
    }

    private async Task CloseConnectionAsync()
    {
        if (_dbConnection is SqlConnection sqlConnection)
        {
            await sqlConnection.CloseAsync();
        }
        else
        {
            _dbConnection?.Close();
        }

        _dbConnection?.Dispose();
        _dbConnection = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _dbConnection?.Dispose();
            }

            _disposed = true;
        }
    }
}
