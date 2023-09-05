namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;

using InMemory;
using Microsoft.Extensions.DependencyInjection;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus();
}
