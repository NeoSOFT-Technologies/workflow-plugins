using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Elsa.Server.Extensions
{
    public static class HealthcheckExtensionRegistration
    {
        public static IServiceCollection AddHealthcheckExtensionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMongoDb(configuration.GetValue<string>("ConnectionStrings:ElsaDb"), tags: new[] {
                        "db",
                        "all"})
                .AddRabbitMQ(configuration.GetValue<string>("ConnectionStrings:RabbitMq"), name: "RabbitMQ", tags: new[] { "Service Bus Broker" })
                .AddRedis(configuration.GetValue<string>("ConnectionStrings:Redis"), name: "Redis", tags: new[] { "Distributed Cache Signal Provider", "Distributed Lock Provider" });
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
                opt.AddHealthCheckEndpoint("API", $"http://{Dns.GetHostName()}/healthz"); //map health check api
            }).AddInMemoryStorage();
            return services;
        }
    }
}
