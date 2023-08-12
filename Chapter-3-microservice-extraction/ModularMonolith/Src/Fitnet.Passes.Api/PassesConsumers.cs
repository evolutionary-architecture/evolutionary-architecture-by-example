namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using Microsoft.Extensions.DependencyInjection;
using RegisterPass;

internal static class PassesConsumers
{
    internal static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.RegisterContractSignedEventConsumer();

        return services;
    }
}