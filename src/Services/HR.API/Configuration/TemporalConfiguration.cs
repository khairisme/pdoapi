using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.API.Configuration
{
    public static class TemporalConfiguration
    {
        public static IServiceCollection AddTemporalServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Get Temporal configuration from settings
            var temporalServerUrl = configuration["Temporal:ServerUrl"] ?? "localhost:7233";
            var taskQueue = configuration["Temporal:TaskQueue"] ?? "hr-workflow-queue";

            // Log the Temporal configuration
            services.AddSingleton(sp => 
            {
                var logger = sp.GetRequiredService<Microsoft.Extensions.Logging.ILoggerFactory>()
                    .CreateLogger("TemporalConfiguration");
                logger.LogInformation("Temporal server URL: {TemporalServerUrl}, Task queue: {TaskQueue}", temporalServerUrl, taskQueue);
                return new TemporalSettings
                {
                    ServerUrl = temporalServerUrl,
                    TaskQueue = taskQueue
                };
            });

            return services;
        }
        
        /// <summary>
        /// Settings for Temporal.io integration
        /// </summary>
        public class TemporalSettings
        {
            /// <summary>
            /// Temporal server URL
            /// </summary>
            public string ServerUrl { get; set; } = string.Empty;
            
            /// <summary>
            /// Temporal task queue
            /// </summary>
            public string TaskQueue { get; set; } = string.Empty;
        }
    }
}
