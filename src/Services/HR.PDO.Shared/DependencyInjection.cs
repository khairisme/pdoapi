

using HR.PDO.Application.Interfaces;
using HR.PDO.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HR.PDO.Shared;

/// <summary>
/// Extension methods for setting up application services in an IServiceCollection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add application services to the specified IServiceCollection
    /// </summary>
    public static IServiceCollection AddSharedApplication(this IServiceCollection services)
    {
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
