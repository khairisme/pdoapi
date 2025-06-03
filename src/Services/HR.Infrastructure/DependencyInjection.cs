using HR.Core.Interfaces;
using HR.Infrastructure.Data;
using HR.Infrastructure.Data.EntityFramework;
using HR.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Infrastructure;

/// <summary>
/// Extension methods for setting up infrastructure services in an IServiceCollection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add infrastructure services to the specified IServiceCollection using the default implementation (Dapper)
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // By default, use Dapper implementation
        return services.AddDapperInfrastructure(configuration);
    }
    
    /// <summary>
    /// Add infrastructure services using Dapper implementation
    /// </summary>
    public static IServiceCollection AddDapperInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register database connection
        services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
        
        // Register unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repository and UnitOfWork registrations are sufficient for the infrastructure layer
        return services;
    }
    
    /// <summary>
    /// Add infrastructure services using Entity Framework Core implementation
    /// </summary>
    public static IServiceCollection AddEntityFrameworkInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<HRDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(HRDbContext).Assembly.FullName)));
        
        // Register repositories
        //services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();
        
       

        // Repository and UnitOfWork registrations are sufficient for the infrastructure layer

        // PNS module registration
        services.AddDbContext<PNSDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("PNSConnection"),
                b => b.MigrationsAssembly(typeof(PNSDbContext).Assembly.FullName)));


        // PDO module registration
        services.AddDbContext<PDODbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("PDOConnection"),
                b => b.MigrationsAssembly(typeof(PDODbContext).Assembly.FullName)));


        // Register unit of work
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<EfUnitOfWork>();
        services.AddScoped<IPNSUnitOfWork, EfPNSUnitOfWork>();
        services.AddScoped<EfPNSUnitOfWork>();

        services.AddScoped<IPDOUnitOfWork, EfPDOUnitOfWork>();
        services.AddScoped<EfPDOUnitOfWork>();

        return services;
    }

   
}
