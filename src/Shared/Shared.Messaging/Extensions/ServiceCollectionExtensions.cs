using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Services;

namespace Shared.Messaging.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQMessageBus(
            this IServiceCollection services,
            string hostname,
            string username,
            string password)
        {
            services.AddSingleton<IMessageBus>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQMessageBus>>();
                return new RabbitMQMessageBus(logger, hostname, username, password);
            });

            return services;
        }
    }
}
