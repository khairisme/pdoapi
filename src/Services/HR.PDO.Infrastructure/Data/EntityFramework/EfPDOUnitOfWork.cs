using HR.PDO.Application.Interfaces;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Infrastructure.Data.EntityFramework
{
    public class EfPDOUnitOfWork : IPDOUnitOfWork
    {
        private readonly PDODbContext _dbContext;
        private IDbContextTransaction? _transaction;
        private readonly ICurrentUserService _currentUserService;
        private bool _disposed;

        // Dictionary to store repositories
        private readonly Dictionary<Type, object> _repositories = new();

        public EfPDOUnitOfWork(PDODbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public IRepository<T> Repository<T>() where T : PDOBaseEntity
        {
            // Check if repository exists, if not create it
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {

                // Default to generic repository if no specific implementation
                _repositories[type] = new EfPDORepository<T>(_dbContext, _currentUserService);
            }

            return (EfPDORepository<T>)_repositories[type];
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
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
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
