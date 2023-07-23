namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.External;

using Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

internal static class ExternalEventBusModule
{
    private const string ExternalEventBusConfiguration = "ExternalEventBus";
    
    internal static IServiceCollection AddExternalEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExternalEventBusOptions>(options => configuration.GetSection(ExternalEventBusConfiguration).Bind(options));

        var serviceProvider = services.BuildServiceProvider();
        var endpoints = serviceProvider.GetRequiredService<IEnumerable<ConsumerConfiguration>>().ToList();
        services.AddMassTransit(configurator =>
        {
            RegisterConsumers(endpoints, configurator);

            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                var options = context.GetRequiredService<IOptions<ExternalEventBusOptions>>();
                var externalEventBusConfigured = options.Value is not null;
                if(!externalEventBusConfigured)
                    return;
                
                ConfigureRabbitMq(options, factoryConfigurator);
                ConfigureConsumers(endpoints, factoryConfigurator, context);

                factoryConfigurator.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IExternalEventBus, ExternalEventBus>();

        return services;
    }
    
    private static void RegisterConsumers(IEnumerable<ConsumerConfiguration> endpoints, IRegistrationConfigurator configurator)
    {
        foreach (var endpoint in endpoints)
        {
            configurator.AddConsumer(endpoint.ConsumerType);
        }
    }
    
    private static void ConfigureRabbitMq(IOptions<ExternalEventBusOptions> options, IRabbitMqBusFactoryConfigurator factoryConfigurator)
    {
        factoryConfigurator.Host(options.Value.Uri, rabbitMqHostConfigurator =>
        {
            rabbitMqHostConfigurator.Username(options.Value.Username);
            rabbitMqHostConfigurator.Password(options.Value.Password);
        });
    }
    private static void ConfigureConsumers(IEnumerable<ConsumerConfiguration> endpoints, IRabbitMqBusFactoryConfigurator factoryConfigurator, IBusRegistrationContext context)
    {
        foreach (var endpoint in endpoints)
        {
            factoryConfigurator.ReceiveEndpoint(endpoint.QueueName, mqReceiveEndpointConfigurator =>
            {
                mqReceiveEndpointConfigurator.ConfigureConsumer(context, endpoint.ConsumerType);
            });
        }
    }
}