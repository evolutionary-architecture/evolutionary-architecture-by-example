namespace EvolutionaryArchitecture.Fitnet.Passes.Api.Common.EventBus;

using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Outbox;

internal static class EventBusModule
{
    private const string EventBusConfiguration = "EventBus";
    private const string RabbitMqConnectionName = "rabbitmq";

    internal static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventBusOptions>(options => configuration.GetSection(EventBusConfiguration).Bind(options));
        services.AddMassTransit(configurator =>
        {
            configurator.SetSnakeCaseEndpointNameFormatter();
            configurator.AddConsumers(Assembly.GetExecutingAssembly());
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                // Try to get Aspire connection string first
                var connectionString = configuration.GetConnectionString(RabbitMqConnectionName);
                
                if (!string.IsNullOrEmpty(connectionString))
                {
                    // Use Aspire connection string
                    factoryConfigurator.Host(new Uri(connectionString));
                }
                else
                {
                    // Fallback to legacy configuration
                    var options = context.GetRequiredService<IOptions<EventBusOptions>>();
                    if (options.Value is not null && !string.IsNullOrEmpty(options.Value.Uri))
                    {
                        factoryConfigurator.Host(options.Value.Uri, hostConfigurator =>
                        {
                            if (!string.IsNullOrEmpty(options.Value.Username))
                            {
                                hostConfigurator.Username(options.Value.Username);
                            }
                            if (!string.IsNullOrEmpty(options.Value.Password))
                            {
                                hostConfigurator.Password(options.Value.Password);
                            }
                        });
                    }
                }
                
                factoryConfigurator.ConfigureEndpoints(context);
            });
            configurator.ConfigureOutbox();
        });

        return services;
    }
}
