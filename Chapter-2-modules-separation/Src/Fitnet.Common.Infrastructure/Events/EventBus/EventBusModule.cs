namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;

using InMemory;
using Microsoft.Extensions.DependencyInjection;

public static class EventBusModule
{
    public static IServiceCollection AddEventBus(this IServiceCollection services) => 
        services.AddInMemoryEventBus();
}