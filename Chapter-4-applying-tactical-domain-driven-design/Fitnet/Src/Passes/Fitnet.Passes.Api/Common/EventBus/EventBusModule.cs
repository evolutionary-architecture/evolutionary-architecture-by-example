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

    internal static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventBusOptions>(options => configuration.GetSection(EventBusConfiguration).Bind(options));
        services.AddMassTransit(configurator =>
        {
            configurator.SetSnakeCaseEndpointNameFormatter();
            configurator.AddConsumers(Assembly.GetExecutingAssembly());
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                var options = context.GetRequiredService<IOptions<EventBusOptions>>();
                var externalEventBusConfigured = options.Value is not null;
                if (!externalEventBusConfigured)
                {
                    return;
                }
                
                var uri = options.Value.Uri;
                var username = options.Value.Username;
                var password = options.Value.Password;
                
                if (!string.IsNullOrEmpty(uri))
                {
                    factoryConfigurator.Host(uri, h =>
                    {
                        if (!string.IsNullOrEmpty(username))
                        {
                            h.Username(username);
                        }
                        if (!string.IsNullOrEmpty(password))
                        {
                            h.Password(password);
                        }
                    });
                }
                
                factoryConfigurator.ConfigureEndpoints(context);
            });
            configurator.ConfigureOutbox();
        });

        return services;
    }
}
