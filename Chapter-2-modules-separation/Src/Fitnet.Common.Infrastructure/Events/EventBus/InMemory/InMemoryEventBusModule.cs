namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.InMemory;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class InMemoryEventBusModule
{
    public static IServiceCollection AddInMemoryEventBus(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        
        return services;
    }
}