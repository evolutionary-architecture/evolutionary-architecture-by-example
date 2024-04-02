namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure;

using Clock;
using Events.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

public static class CommonInfrastructureModule
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddEventBus();
        services.AddFeatureManagement();
        services.AddClock();

        return services;
    }
}
