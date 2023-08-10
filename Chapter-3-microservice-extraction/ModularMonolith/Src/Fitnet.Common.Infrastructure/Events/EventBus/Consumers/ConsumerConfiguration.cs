namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.Consumers;

using MassTransit;

internal sealed class ConsumerConfiguration
{
    private ConsumerConfiguration(string queueName, Type consumerType)
    {
        if (consumerType.GetInterface(nameof(IConsumer)) is null)
        {
            throw new ArgumentException($"{consumerType.FullName} must implement {typeof(IConsumer).FullName}", nameof(consumerType));
        }

        QueueName = queueName;
        ConsumerType = consumerType;
    }
    
    internal static ConsumerConfiguration Configure(string queueName, Type consumerType) => 
        new(queueName, consumerType);

    internal string QueueName { get; }
    internal Type ConsumerType { get; }
}