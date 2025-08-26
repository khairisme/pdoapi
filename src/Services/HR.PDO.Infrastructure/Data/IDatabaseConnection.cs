using System.Data;

namespace HR.PDO.Infrastructure.Data;

/// <summary>
/// Interface for database connection factory
/// </summary>
public interface IDatabaseConnection
{
    /// <summary>
    /// Create a new database connection
    /// </summary>
    IDbConnection CreateConnection();
}
