using System.Linq.Expressions;

namespace HR.PDO.Core.Interfaces;

/*
 * ARCHITECTURAL NOTE:
 * This interface is intentionally placed in HR.PDO.Core rather than Shared.Common to:
 * 1. Maintain clean architecture principles (domain defines what it needs)
 * 2. Avoid circular dependencies between projects
 * 3. Keep domain-specific data access patterns close to the domain
 * 4. Simplify maintenance and evolution of the interface as domain needs change
 *
 * Moving this to Shared.Common would add complexity unless there's a specific need
 * to share this exact interface across multiple bounded contexts or microservices.
 */

/// <summary>
/// Generic repository interface for CRUD operations and query access.
/// Provides methods for adding, updating, deleting, counting, and retrieving entities,
/// as well as advanced search and paging functionality.
/// </summary>
/// <typeparam name="T">The entity type this repository manages.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get an entity by its GUID identifier.
    /// </summary>
    /// <param name="id">The GUID of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Get an entity by its integer identifier.
    /// </summary>
    /// <param name="id">The integer ID of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Get all entities.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Find entities matching a specified predicate.
    /// </summary>
    /// <param name="predicate">The expression to filter entities.</param>
    /// <returns>A collection of entities that match the predicate.</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Add a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Add multiple new entities.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <returns>The added entities.</returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Update an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>True if the update succeeds; otherwise, false.</returns>
    Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// Delete an entity by its GUID identifier.
    /// </summary>
    /// <param name="id">The GUID of the entity to delete.</param>
    /// <returns>True if the deletion succeeds; otherwise, false.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Delete an entity by its integer identifier.
    /// </summary>
    /// <param name="id">The integer ID of the entity to delete.</param>
    /// <returns>True if the deletion succeeds; otherwise, false.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Get the total number of entities.
    /// </summary>
    /// <returns>The count of entities.</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Check if an entity exists by its GUID identifier.
    /// </summary>
    /// <param name="id">The GUID of the entity.</param>
    /// <returns>True if the entity exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(Guid id);

    /// <summary>
    /// Get a paged list of entities.
    /// </summary>
    /// <param name="pageNumber">The page number (1-based).</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns>A tuple containing the items in the page and the total entity count.</returns>
    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Search entities by a string term for text-based searches.
    /// </summary>
    /// <param name="searchTerm">The search term.</param>
    /// <returns>A collection of entities matching the search term.</returns>
    Task<IEnumerable<T>> SearchByTermAsync(string searchTerm);

    /// <summary>
    /// Find entities by a specific field value.
    /// </summary>
    /// <param name="fieldName">The name of the field to search.</param>
    /// <param name="fieldValue">The value to match.</param>
    /// <returns>A collection of entities matching the field value.</returns>
    Task<IEnumerable<T>> FindByFieldAsync(string fieldName, object fieldValue);

    /// <summary>
    /// Get the first entity that matches a given predicate, or null if none exist.
    /// </summary>
    /// <param name="predicate">The expression to filter entities.</param>
    /// <returns>The first matching entity or null.</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Find entities by a specific field value without filtering by StatusAktif.
    /// </summary>
    /// <param name="fieldName">The name of the field to search.</param>
    /// <param name="fieldValue">The value to match.</param>
    /// <returns>A collection of entities matching the field value.</returns>
    Task<IEnumerable<T>> FindByFieldWithoutStatusAktifAsync(string fieldName, object fieldValue);

    /// <summary>
    /// Provides access to the underlying IQueryable for advanced queries.
    /// Use this to write LINQ queries that will be translated to SQL.
    /// </summary>
    /// <returns>An IQueryable of the entity type.</returns>
    IQueryable<T> Query();
}

