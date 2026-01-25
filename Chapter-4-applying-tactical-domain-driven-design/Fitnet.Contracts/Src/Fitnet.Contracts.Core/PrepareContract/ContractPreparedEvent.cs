namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.PrepareContract;

using Common.Core;

public sealed record ContractPreparedEvent(
    Guid Id,
    Guid CustomerId,
    DateTimeOffset PreparedAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static ContractPreparedEvent Raise(
        Guid customerId,
        DateTimeOffset preparedAt)
        => new(
            Guid.NewGuid(),
            customerId,
            preparedAt,
            DateTime.UtcNow);
}
