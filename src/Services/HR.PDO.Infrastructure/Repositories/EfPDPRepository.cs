using System.Linq.Expressions;
using HR.PDO.Application.Interfaces;
using HR.PDO.Core.Entities.PDP;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace HR.PDO.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using Entity Framework Core
/// </summary>
public class EfPDPRepository<T> : IRepository<T> where T : PDPBaseEntity
{
    protected readonly PDPDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;
    protected readonly ICurrentUserService _currentUserService;

    public EfPDPRepository(PDPDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
        _currentUserService = currentUserService;
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(e =>  Convert.ToBoolean(e.StatusAktif));
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync(e => Convert.ToBoolean(e.StatusAktif));
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Where(e => Convert.ToBoolean(e.StatusAktif)).ToListAsync();
    }
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        // Combine the predicate with IsDeleted filter
        var combinedPredicate = PredicateBuilder.And(predicate, e => Convert.ToBoolean(e.StatusAktif));
        return await _dbSet.Where(combinedPredicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        //entity.IdCipta = System.Guid.NewGuid();
        entity.IdCipta = Guid.TryParse(_currentUserService.UserId, out var userId) ? userId : Guid.Empty;
        entity.TarikhCipta = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);
        return entity;
    }
    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.IdCipta= Guid.TryParse(_currentUserService.UserId, out var userId) ? userId : Guid.Empty;
            entity.TarikhCipta = DateTime.UtcNow;
        }

        await _dbSet.AddRangeAsync(entities);
        return entities;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        entity.TarikhPinda = DateTime.UtcNow;
        entity.IdPinda = Guid.TryParse(_currentUserService.UserId, out var userId) ? userId : Guid.Empty;
        // Detach any existing entity with the same ID to avoid conflicts
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).State = EntityState.Detached;
        }

        _dbContext.Entry(entity).State = EntityState.Modified;
        // Don't modify CreatedAt or CreatedBy
        _dbContext.Entry(entity).Property(x => x.TarikhCipta).IsModified = false;
        _dbContext.Entry(entity).Property(x => x.IdCipta).IsModified = false;
        

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // Soft delete - update IsDeleted flag
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        entity.StatusAktif = false;
        entity.TarikhPinda = DateTime.UtcNow;
        entity.IdPinda = Guid.TryParse(_currentUserService.UserId, out var userId) ? userId : Guid.Empty;

        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        // Soft delete - update IsDeleted flag
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        entity.StatusAktif = false;
        entity.TarikhPinda = DateTime.UtcNow;
        entity.IdPinda = Guid.TryParse(_currentUserService.UserId, out var userId) ? userId : Guid.Empty;

        return true;
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(e => Convert.ToBoolean(e.StatusAktif));
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync(e => Convert.ToBoolean(e.StatusAktif));
    }

   
    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize)
    {
        var query = _dbSet.Where(e => Convert.ToBoolean(e.StatusAktif));
        
        // Get total count
        var totalCount = await query.CountAsync();
        
        // Get paged data
        var items = await query
            .OrderByDescending(e => e.TarikhCipta)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return (items, totalCount);
    }
    
    public async Task<IEnumerable<T>> SearchByTermAsync(string searchTerm)
    {
        // Get properties that might contain searchable text
        var stringProperties = typeof(T).GetProperties()
            .Where(p => p.PropertyType == typeof(string))
            .ToList();
            
        if (stringProperties.Count == 0)
        {
            // If no string properties, return empty result
            return Enumerable.Empty<T>();
        }
        
        // Build a query that searches across all string properties
        var query = _dbSet.Where(e => Convert.ToBoolean(e.StatusAktif));
        
        // Create a parameter for the entity
        var parameter = Expression.Parameter(typeof(T), "e");
        
        // Create a condition for each string property
        Expression? combinedExpression = null;
        foreach (var prop in stringProperties)
        {
            // Create property access: e.PropertyName
            var propertyAccess = Expression.Property(parameter, prop);
            
            // Create method call: e.PropertyName.Contains(searchTerm)
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var searchTermConstant = Expression.Constant(searchTerm, typeof(string));
            var containsCall = Expression.Call(propertyAccess, containsMethod, searchTermConstant);
            
            // Add null check: e.PropertyName != null && e.PropertyName.Contains(searchTerm)
            var nullCheck = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            var condition = Expression.AndAlso(nullCheck, containsCall);
            
            // Combine with OR
            if (combinedExpression == null)
            {
                combinedExpression = condition;
            }
            else
            {
                combinedExpression = Expression.OrElse(combinedExpression, condition);
            }
        }
        
        // If we have no valid properties to search, return all non-deleted entities
        if (combinedExpression == null)
        {
            return await query.ToListAsync();
        }
        
        // Create and compile the lambda expression
        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        
        // Apply the filter
        return await query.Where(lambda).ToListAsync();
    }
    
    public async Task<IEnumerable<T>> FindByFieldAsync(string fieldName, object fieldValue)
    {
        // Validate that the field exists on the entity
        var property = typeof(T).GetProperty(fieldName);
        if (property == null)
        {
            throw new ArgumentException($"Field '{fieldName}' does not exist on entity {typeof(T).Name}");
        }
        
        // Create a parameter for the entity
        var parameter = Expression.Parameter(typeof(T), "e");
        
        // Create property access: e.PropertyName
        var propertyAccess = Expression.Property(parameter, property);
        
        // Create constant for the value
        var valueConstant = Expression.Constant(fieldValue, property.PropertyType);
        
        // Create equality comparison: e.PropertyName == fieldValue
        var equalityComparison = Expression.Equal(propertyAccess, valueConstant);
        
        // Create lambda expression: e => e.PropertyName == fieldValue
        var lambda = Expression.Lambda<Func<T, bool>>(equalityComparison, parameter);
        
        // Apply the filter along with IsDeleted check
        var combinedPredicate = PredicateBuilder.And(lambda, e => Convert.ToBoolean(e.StatusAktif));
        return await _dbSet.Where(combinedPredicate).ToListAsync();
    }
    public async Task<IEnumerable<T>> FindByFieldWithoutStatusAktifAsync(string fieldName, object fieldValue)
    {
        var property = typeof(T).GetProperty(fieldName);
        if (property == null)
        {
            throw new ArgumentException($"Field '{fieldName}' does not exist on entity {typeof(T).Name}");
        }

        var parameter = Expression.Parameter(typeof(T), "e");
        var propertyAccess = Expression.Property(parameter, property);
        var valueConstant = Expression.Constant(fieldValue, property.PropertyType);
        var equalityComparison = Expression.Equal(propertyAccess, valueConstant);
        var lambda = Expression.Lambda<Func<T, bool>>(equalityComparison, parameter);

        return await _dbSet.Where(lambda).ToListAsync();
    }
}

