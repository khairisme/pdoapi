using System.Linq.Expressions;

namespace HR.Core.Interfaces;

/*
 * ARCHITECTURAL NOTE:
 * This interface is intentionally placed in HR.Core rather than Shared.Common to:
 * 1. Maintain clean architecture principles (domain defines what it needs)
 * 2. Avoid circular dependencies between projects
 * 3. Keep domain-specific data access patterns close to the domain
 * 4. Simplify maintenance and evolution of the interface as domain needs change
 *
 * Moving this to Shared.Common would add complexity unless there's a specific need
 * to share this exact interface across multiple bounded contexts or microservices.
 */

/// <summary>
/// Generic repository interface for CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get entity by id
    /// </summary>
    Task<T?> GetByIdAsync(Guid id);

    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Get all entities
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Find entities by predicate
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Add a new entity
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Add a new entity
    /// </summary>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Update an existing entity
    /// </summary>
    Task<bool> UpdateAsync(T entity);
    
    /// <summary>
    /// Delete an entity
    /// </summary>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Delete an entity
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Count entities
    /// </summary>
    Task<int> CountAsync();
    
    /// <summary>
    /// Check if entity exists
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
    
    /// <summary>
    /// Get paged list of entities
    /// </summary>
    /// <param name="pageNumber">Page number (1-based)</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Tuple containing the paged entities and total count</returns>
    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize);
    
    /// <summary>
    /// Search entities by a string search term (for text-based searches)
    /// </summary>
    /// <param name="searchTerm">The search term to look for</param>
    /// <returns>Collection of entities matching the search term</returns>
    Task<IEnumerable<T>> SearchByTermAsync(string searchTerm);
    
    /// <summary>
    /// Find entities by a specific field value
    /// </summary>
    /// <param name="fieldName">Name of the field to search on</param>
    /// <param name="fieldValue">Value to search for</param>
    /// <returns>Collection of entities matching the field value</returns>
    Task<IEnumerable<T>> FindByFieldAsync(string fieldName, object fieldValue);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> FindByFieldWithoutStatusAktifAsync(string fieldName, object fieldValue);
}
