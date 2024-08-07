namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.InMemory;

using Microsoft.Extensions.DependencyInjection;

public static class InMemoryEventBusModule
{
    public static IServiceCollection AddInMemoryEventBus(this IServiceCollection services)
    {
        services.AddScoped<IEventBus, InMemoryEventBus>();

        return services;
    }
}
