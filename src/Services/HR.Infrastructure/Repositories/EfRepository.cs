using System.Linq.Expressions;
using HR.Core.Entities;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using Entity Framework Core
/// </summary>
public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly HRDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public EfRepository(HRDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        // Combine the predicate with IsDeleted filter
        var combinedPredicate = PredicateBuilder.And(predicate, e => !e.IsDeleted);
        return await _dbSet.Where(combinedPredicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
        entity.CreatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);
        return entity;
    }
    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.CreatedAt = DateTime.UtcNow;
        }

        await _dbSet.AddRangeAsync(entities);
        return entities;
    }
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }
    public async Task<bool> UpdateAsync(T entity)
    {
        entity.ModifiedAt = DateTime.UtcNow;

        // Detach any existing entity with the same ID to avoid conflicts
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).State = EntityState.Detached;
        }

        _dbContext.Entry(entity).State = EntityState.Modified;
        // Don't modify CreatedAt or CreatedBy
        _dbContext.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
        _dbContext.Entry(entity).Property(x => x.CreatedBy).IsModified = false;

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // Soft delete - update IsDeleted flag
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        entity.IsDeleted = true;
        entity.ModifiedAt = DateTime.UtcNow;

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Soft delete - update IsDeleted flag
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        entity.IsDeleted = true;
        entity.ModifiedAt = DateTime.UtcNow;

        return true;
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync(e => !e.IsDeleted);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id && !e.IsDeleted);
    }
    
    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize)
    {
        var query = _dbSet.Where(e => !e.IsDeleted);
        
        // Get total count
        var totalCount = await query.CountAsync();
        
        // Get paged data
        var items = await query
            .OrderByDescending(e => e.CreatedAt)
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
        var query = _dbSet.Where(e => !e.IsDeleted);
        
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
        var combinedPredicate = PredicateBuilder.And(lambda, e => !e.IsDeleted);
        return await _dbSet.Where(combinedPredicate).ToListAsync();
    }
}

/// <summary>
/// Helper class for combining predicates
/// </summary>
public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
