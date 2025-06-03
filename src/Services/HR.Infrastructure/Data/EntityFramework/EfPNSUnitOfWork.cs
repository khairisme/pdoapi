using HR.Core.Entities;
using HR.Core.Interfaces;
using HR.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Data.EntityFramework
{
    public class EfPNSUnitOfWork : IPNSUnitOfWork
    {
        private readonly PNSDbContext _dbContext;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        // Dictionary to store repositories
        private readonly Dictionary<Type, object> _repositories = new();

        public EfPNSUnitOfWork(PNSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>() where T : PNSBaseEntity
        {
            // Check if repository exists, if not create it
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {

                // Default to generic repository if no specific implementation
                _repositories[type] = new EfPNSRepository<T>(_dbContext);
            }

            return (EfPNSRepository<T>)_repositories[type];
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
