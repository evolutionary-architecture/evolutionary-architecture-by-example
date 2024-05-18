namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure;

using Clock;
using Events.EventBus;
using Microsoft.Extensions.DependencyInjection;

public static class CommonInfrastructureModule
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddEventBus();
        services.AddClock();

        return services;
    }
}
