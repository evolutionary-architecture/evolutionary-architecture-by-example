namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Mediation;

using Application;
using Microsoft.Extensions.DependencyInjection;

internal static class MediationModule
{
    internal static IServiceCollection AddMediationModule(this IServiceCollection services)
    {
        var commandsHandlersAssembly = typeof(IContractsModule).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(commandsHandlersAssembly));

        return services;
    }
}