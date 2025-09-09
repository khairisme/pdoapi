using HR.Application.Services.PDO;
using HR.PDO.Application.Interfaces;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
//using HR.PDO.ModuleAPI.Interfaces.ONB;
//using HR.PDO.ModuleAPI.Interfaces.PPA;
//using HR.PDO.ModuleAPI.Services.PPA;
namespace HR.PDO.ModuleAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModuleApi(this IServiceCollection services)
        {
            services.AddHttpContextAccessor(); // <-- important
            services.AddScoped<ICurrentUserService, CurrentUserService>(); services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IPNSUnitOfWork, EfPNSUnitOfWork>();
            services.AddScoped<IPDOUnitOfWork, EfPDOUnitOfWork>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IPDPUnitOfWork, EfPDPUnitOfWork>();
            return services;
        }
    }
}
