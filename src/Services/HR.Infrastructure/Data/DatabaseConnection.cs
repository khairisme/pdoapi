using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HR.Infrastructure.Data;

/// <summary>
/// Database connection factory
/// </summary>
public class DatabaseConnection : IDatabaseConnection
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? 
            throw new ArgumentNullException("DefaultConnection string is not configured");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
