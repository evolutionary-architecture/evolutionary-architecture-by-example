namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;

using Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RedeliveryOptions = Consumers.RedeliveryOptions;

internal static class EventBusModule
{
    private const string EventBusConfiguration = "EventBus";

    internal static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventBusOptions>(options => configuration.GetSection(EventBusConfiguration).Bind(options));

        var serviceProvider = services.BuildServiceProvider();
        var endpoints = serviceProvider.GetRequiredService<IEnumerable<ConsumerConfiguration>>().ToList();
        services.AddMassTransit(configurator =>
        {
            RegisterConsumers(endpoints, configurator);

            configurator.AddPublishMessageScheduler();
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                var options = context.GetRequiredService<IOptions<EventBusOptions>>();
                var externalEventBusConfigured = options.Value is not null;
                if (!externalEventBusConfigured)
                {
                    return;
                }

                ConfigureRabbitMq(options, factoryConfigurator);
                ConfigureConsumers(endpoints, factoryConfigurator, context);

                factoryConfigurator.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IEventBus, EventBus>();

        return services;
    }

    private static void RegisterConsumers(IEnumerable<ConsumerConfiguration> endpoints, IRegistrationConfigurator configurator)
    {
        foreach (var endpoint in endpoints)
        {
            configurator.AddConsumer(endpoint.ConsumerType);
        }
    }

    private static void ConfigureRabbitMq(IOptions<EventBusOptions> options,
        IRabbitMqBusFactoryConfigurator factoryConfigurator) => factoryConfigurator.Host(options.Value.Uri,
        rabbitMqHostConfigurator =>
        {
            rabbitMqHostConfigurator.Username(options.Value.Username);
            rabbitMqHostConfigurator.Password(options.Value.Password);
        });

    private static void ConfigureConsumers(IEnumerable<ConsumerConfiguration> endpoints, IRabbitMqBusFactoryConfigurator factoryConfigurator, IBusRegistrationContext context)
    {
        foreach (var endpoint in endpoints)
        {
            factoryConfigurator.ReceiveEndpoint(endpoint.QueueName, mqReceiveEndpointConfigurator =>
            {
                ConfigureRetry(endpoint.RetryOptions, mqReceiveEndpointConfigurator);
                ConfigureRedelivery(endpoint.RedeliveryOptions, mqReceiveEndpointConfigurator);

                mqReceiveEndpointConfigurator.ConfigureConsumer(context, endpoint.ConsumerType);
            });
        }
    }

    private static void ConfigureRetry(RetryOptions options, IRabbitMqReceiveEndpointConfigurator mqReceiveEndpointConfigurator)
    {
        if (options.Enabled)
        {
            mqReceiveEndpointConfigurator.UseRetry(retryConfigurator => retryConfigurator.Incremental(options.RetryCount, options.RetryInitialInterval, options.RetryIntervalIncrement));
        }
    }

    private static void ConfigureRedelivery(RedeliveryOptions options, IRabbitMqReceiveEndpointConfigurator mqReceiveEndpointConfigurator)
    {
        if (options.Enabled)
        {
            mqReceiveEndpointConfigurator.UseDelayedRedelivery(redeliveryConfigurator => redeliveryConfigurator.Intervals(options.DeliveryIntervals));
        }
    }
}
