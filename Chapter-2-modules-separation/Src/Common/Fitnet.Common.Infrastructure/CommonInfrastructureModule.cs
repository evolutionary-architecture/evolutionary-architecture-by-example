namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure;

using Events.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

public static class CommonInfrastructureModule
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddEventBus();
        services.AddFeatureManagement();

        return services;
    }
}
