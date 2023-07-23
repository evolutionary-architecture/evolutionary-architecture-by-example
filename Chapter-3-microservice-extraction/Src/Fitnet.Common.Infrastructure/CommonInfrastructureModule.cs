namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure;

using Events.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class CommonInfrastructureModule
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventBus(configuration);

        return services;
    }
}