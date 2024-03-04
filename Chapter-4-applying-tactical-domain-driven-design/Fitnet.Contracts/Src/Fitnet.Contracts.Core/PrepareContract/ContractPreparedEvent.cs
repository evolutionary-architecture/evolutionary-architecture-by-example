namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.PrepareContract;

using DomainDrivenDesign.BuildingBlocks;

public sealed record ContractPreparedEvent(
    Guid Id,
    Guid CustomerId,
    DateTimeOffset PreparedAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static ContractPreparedEvent Create(Guid customerId,
        DateTimeOffset preparedAt)
        => new(
            Guid.NewGuid(),
            customerId,
            preparedAt,
            DateTime.Now);
}
