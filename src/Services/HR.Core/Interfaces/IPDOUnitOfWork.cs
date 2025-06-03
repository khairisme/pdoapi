using HR.Core.Entities;

namespace HR.Core.Interfaces;

/*
 * ARCHITECTURAL NOTE:
 * This interface is intentionally placed in HR.Core rather than Shared.Common to:
 * 1. Maintain clean architecture principles (domain defines what it needs)
 * 2. Avoid circular dependencies between projects
 * 3. Keep domain-specific data access patterns close to the domain
 * 4. Preserve the dependency on HR.Core.Entities.BaseEntity
 * 5. Simplify maintenance and evolution of the interface as domain needs change
 *
 * Moving this to Shared.Common would add complexity unless there's a specific need
 * to share this exact interface across multiple bounded contexts or microservices.
 */

/// <summary>
/// Unit of Work interface to manage database transactions
/// </summary>
public interface IPDOUnitOfWork : IDisposable 
{

    /// <summary>
    /// Get repository for entity type
    /// </summary>
    IRepository<T> Repository<T>() where T : PDOBaseEntity;

    /// <summary>
    /// Begin a new transaction
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commit the transaction
    /// </summary>
    Task CommitAsync();

    /// <summary>
    /// Rollback the transaction
    /// </summary>
    Task RollbackAsync();

    /// <summary>
    /// Save all changes made in this context to the database
    /// </summary>
    Task<int> SaveChangesAsync();
}
