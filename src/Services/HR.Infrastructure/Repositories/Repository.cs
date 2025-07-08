using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Dapper;
using HR.Core.Entities;
using HR.Core.Interfaces;
using HR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using Dapper
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IDatabaseConnection _connection;
    protected readonly string _tableName;

    public Repository(IDatabaseConnection connection)
    {
        _connection = connection;
        _tableName = typeof(T).Name;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        using var conn = _connection.CreateConnection();
        var query = $"SELECT * FROM {_tableName} WHERE Id = @Id AND IsDeleted = 0";
        return await conn.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var conn = _connection.CreateConnection();
        var query = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0";
        return await conn.QueryAsync<T>(query);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        // This is a simplified implementation since Dapper doesn't support LINQ expressions directly
        // In a real-world scenario, you might want to use a query builder or a more sophisticated approach
        using var conn = _connection.CreateConnection();
        var query = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0";
        var result = await conn.QueryAsync<T>(query);
        
        // Apply the predicate in memory
        return result.Where(predicate.Compile());
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
        entity.CreatedAt = DateTime.UtcNow;
        
        using var conn = _connection.CreateConnection();
        
        // Get all properties except Id (which is handled separately)
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.Name != "Id" && p.CanWrite);
        
        var columnNames = string.Join(", ", properties.Select(p => p.Name));
        var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
        
        var query = $"INSERT INTO {_tableName} (Id, {columnNames}) VALUES (@Id, {parameterNames}); " +
                   $"SELECT * FROM {_tableName} WHERE Id = @Id";
        
        var result = await conn.QueryFirstOrDefaultAsync<T>(query, entity);
        return result ?? entity;
    }
    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.CreatedAt = DateTime.UtcNow;
        }

       
        return entities;
    }
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return  null;
    }
    public async Task<bool> UpdateAsync(T entity)
    {
        entity.ModifiedAt = DateTime.UtcNow;
        
        using var conn = _connection.CreateConnection();
        
        // Get all properties except Id and CreatedAt (which shouldn't be updated)
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.Name != "Id" && p.Name != "CreatedAt" && p.Name != "CreatedBy" && p.CanWrite);
        
        var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        
        var query = $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id";
        
        var rowsAffected = await conn.ExecuteAsync(query, entity);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // Soft delete
        using var conn = _connection.CreateConnection();
        var query = $"UPDATE {_tableName} SET IsDeleted = 1, ModifiedAt = @ModifiedAt WHERE Id = @Id";
        var rowsAffected = await conn.ExecuteAsync(query, new { Id = id, ModifiedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        // Soft delete
        using var conn = _connection.CreateConnection();
        var query = $"UPDATE {_tableName} SET IsDeleted = 1, ModifiedAt = @ModifiedAt WHERE Id = @Id";
        var rowsAffected = await conn.ExecuteAsync(query, new { Id = id, ModifiedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }

    public async Task<int> CountAsync()
    {
        using var conn = _connection.CreateConnection();
        var query = $"SELECT COUNT(*) FROM {_tableName} WHERE IsDeleted = 0";
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        using var conn = _connection.CreateConnection();
        var query = $"SELECT COUNT(1) FROM {_tableName} WHERE Id = @Id AND IsDeleted = 0";
        var count = await conn.ExecuteScalarAsync<int>(query, new { Id = id });
        return count > 0;
    }
    
    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize)
    {
        using var conn = _connection.CreateConnection();
        
        // Get total count
        var countQuery = $"SELECT COUNT(*) FROM {_tableName} WHERE IsDeleted = 0";
        var totalCount = await conn.ExecuteScalarAsync<int>(countQuery);
        
        // Get paged data
        // Note: SQL Server 2012+ supports OFFSET/FETCH
        var query = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0 ORDER BY CreatedAt DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        var offset = (pageNumber - 1) * pageSize;
        var items = await conn.QueryAsync<T>(query, new { Offset = offset, PageSize = pageSize });
        
        return (items, totalCount);
    }
    
    public async Task<IEnumerable<T>> SearchByTermAsync(string searchTerm)
    {
        using var conn = _connection.CreateConnection();
        
        // Get properties that might contain searchable text
        var properties = typeof(T).GetProperties()
            .Where(p => p.PropertyType == typeof(string))
            .Select(p => p.Name)
            .ToList();
            
        if (properties.Count == 0)
        {
            // If no string properties, return empty result
            return Enumerable.Empty<T>();
        }
        
        // Build a query that searches across all string properties
        var conditions = string.Join(" OR ", properties.Select(p => $"{p} LIKE @SearchPattern"));
        var query = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0 AND ({conditions})";
        
        // Use LIKE with wildcards for partial matching
        return await conn.QueryAsync<T>(query, new { SearchPattern = $"%{searchTerm}%" });
    }
    
    public async Task<IEnumerable<T>> FindByFieldAsync(string fieldName, object fieldValue)
    {
        using var conn = _connection.CreateConnection();
        
        // Validate that the field exists on the entity
        var property = typeof(T).GetProperty(fieldName);
        if (property == null)
        {
            throw new ArgumentException($"Field '{fieldName}' does not exist on entity {typeof(T).Name}");
        }
        
        var query = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0 AND {fieldName} = @FieldValue";
        var parameters = new DynamicParameters();
        parameters.Add("FieldValue", fieldValue);
        
        return await conn.QueryAsync<T>(query, parameters);
    }
}
