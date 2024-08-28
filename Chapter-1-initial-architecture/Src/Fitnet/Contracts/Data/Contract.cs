namespace EvolutionaryArchitecture.Fitnet.Contracts.Data;


internal sealed class Contract
{
    private static TimeSpan StandardDuration => TimeSpan.FromDays(365);

    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    public DateTimeOffset PreparedAt { get; init; }
    public TimeSpan Duration { get; init; }

    public DateTimeOffset? SignedAt { get; set; }
    public DateTimeOffset? ExpiringAt { get; set; }

    public bool Signed => SignedAt.HasValue;

    // ReSharper disable once ConvertToPrimaryConstructor
#pragma warning disable IDE0290
    public Contract(Guid id,
#pragma warning restore IDE0290
        Guid customerId,
        DateTimeOffset preparedAt,
        TimeSpan duration)
    {
        Id = id;
        CustomerId = customerId;
        PreparedAt = preparedAt;
        Duration = duration;
    }

    internal static Contract Create(Guid customerId, DateTimeOffset preparedAt) => new(Guid.NewGuid(),
            customerId,
            preparedAt,
            StandardDuration);
}
