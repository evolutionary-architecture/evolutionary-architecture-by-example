namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Mediator;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class MediatorModule
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
