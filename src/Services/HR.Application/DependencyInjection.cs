using HR.Application.Interfaces;
using HR.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Application;

/// <summary>
/// Extension methods for setting up application services in an IServiceCollection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add application services to the specified IServiceCollection
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register default service implementation
        services.AddScoped<IEmployeeService, EmployeeService>();
        
        // Register specialized service implementations
        services.AddScoped<EfEmployeeService>();

        services.AddScoped<IPenggunaService, PenggunaService>();

        services.AddScoped<IKumpulanPerkhidmatanService, KumpulanPerkhidmatanService>();
        services.AddScoped<IRujStatusPermohonanService, RujStatusPermohonanService>();
        services.AddScoped<IMaklumatKlasifikasiPerkhidmatanService, MaklumatKlasifikasiPerkhidmatanService>();

        return services;
    }
}
