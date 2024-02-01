namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

public sealed class BindingContract
{
    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    public TimeSpan Duration { get; init; }

    private BindingContract(Guid id,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset? expiringAt)
    {
        Id = id;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
    }

    internal static BindingContract Start(Guid id, Guid customerId, TimeSpan duration, DateTimeOffset? expiringAt) =>
        new(id, customerId, duration, expiringAt);

    public DateTimeOffset? ExpiringAt { get; set; }
}
