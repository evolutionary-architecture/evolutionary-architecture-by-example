namespace EvolutionaryArchitecture.Fitnet.Passes.Api.Common.EventBus;

using DataAccess.Database;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

internal static class EventBusModule
{
    private const string EventBusConfiguration = "EventBus";

    internal static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventBusOptions>(options => configuration.GetSection(EventBusConfiguration).Bind(options));
        services.AddMassTransit<IPassesBus>(configurator =>
        {
            configurator.AddEntityFrameworkOutbox<PassesPersistence>(o =>
            {
                o.UsePostgres();
                o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
            });
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                var options = context.GetRequiredService<IOptions<EventBusOptions>>();
                var externalEventBusConfigured = options.Value is not null;
                if (!externalEventBusConfigured)
                {
                    return;
                }

                factoryConfigurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
