namespace EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;

using System.Reflection;
using InMemory;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
}
