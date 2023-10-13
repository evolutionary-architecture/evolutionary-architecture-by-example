namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.Consumers;

using MassTransit;

public sealed class ConsumerConfiguration
{
    private ConsumerConfiguration(string queueName, Type consumerType, RetryOptions? retryOptions = null, RedeliveryOptions? redeliveryOptions = null)
    {
        if (consumerType.GetInterface(nameof(IConsumer)) is null)
        {
            throw new ArgumentException($"{consumerType.FullName} must implement {typeof(IConsumer).FullName}", nameof(consumerType));
        }

        QueueName = queueName;
        ConsumerType = consumerType;
        RetryOptions = retryOptions ?? RetryOptions.Disabled;
        RedeliveryOptions = redeliveryOptions ?? RedeliveryOptions.Disabled;
    }

    public static ConsumerConfiguration Configure(string queueName, Type consumerType, RetryOptions? retryOptions = null, RedeliveryOptions? redeliveryOptions = null) =>
        new(queueName, consumerType, retryOptions ?? RetryOptions.Disabled, redeliveryOptions ?? RedeliveryOptions.Disabled);

    internal string QueueName { get; }
    internal Type ConsumerType { get; }
    internal RetryOptions RetryOptions { get; }
    internal RedeliveryOptions RedeliveryOptions { get; }
}

public record struct RetryOptions(bool Enabled, int RetryCount, TimeSpan RetryInitialInterval, TimeSpan RetryIntervalIncrement)
{
    internal static RetryOptions Disabled => new(false, 0, TimeSpan.Zero, TimeSpan.Zero);
}

public record struct RedeliveryOptions(bool Enabled, TimeSpan[] DeliveryIntervals)
{
    internal static RedeliveryOptions Disabled => new(false, Array.Empty<TimeSpan>());
}
