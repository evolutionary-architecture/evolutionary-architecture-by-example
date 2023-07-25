namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.Consumers;

using Microsoft.Extensions.DependencyInjection;

public static class ConsumerRegisterExtensions
{
    public static IServiceCollection RegisterConsumer(this IServiceCollection serviceCollection, string queueName, Type consumerType)
    {
        var consumerConfiguration = ConsumerConfiguration.Configure(queueName, consumerType);
        serviceCollection.AddSingleton(consumerConfiguration);
        
        return serviceCollection;
    }
}