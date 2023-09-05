namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess;

public sealed class Pass
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTimeOffset From { get; init; }
    public DateTimeOffset To { get; private set; }

    private Pass(Guid id, Guid customerId, DateTimeOffset from, DateTimeOffset to)
    {
        Id = id;
        CustomerId = customerId;
        From = from;
        To = to;
    }

    public static Pass Register(Guid customerId, DateTimeOffset from, DateTimeOffset to) =>
        new(Guid.NewGuid(), customerId, from, to);

    public void MarkAsExpired(DateTimeOffset nowDateTimeOffset) => To = nowDateTimeOffset;
}
