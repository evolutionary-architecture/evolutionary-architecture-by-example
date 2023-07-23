namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;

using External;
using InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInMemoryEventBus();
        services.AddExternalEventBus(configuration);

        return services;
    }
}