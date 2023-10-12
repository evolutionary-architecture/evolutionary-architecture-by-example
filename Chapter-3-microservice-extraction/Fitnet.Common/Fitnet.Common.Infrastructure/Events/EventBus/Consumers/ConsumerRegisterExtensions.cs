namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.Consumers;

using Microsoft.Extensions.DependencyInjection;

public static class ConsumerRegisterExtensions
{
    public static IServiceCollection RegisterConsumer(this IServiceCollection serviceCollection, ConsumerConfiguration consumerConfiguration)
    {
        serviceCollection.AddSingleton(consumerConfiguration);

        return serviceCollection;
    }
}
