namespace SuperSimpleArchitecture.Fitnet.Shared.Events.EventBus;

using InMemory;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) => 
        services.AddInMemoryEventBus();
}