namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Common.Infrastructure.Events.EventBus.Consumers;
using Microsoft.Extensions.DependencyInjection;

internal static class ContractSignedRegistration
{
    internal static IServiceCollection RegisterContractSignedEventConsumer(this IServiceCollection services)
    {
        services.RegisterConsumer(
            "contracts-signed", 
            typeof(ContractSignedEventConsumer));

        return services;
    }
}