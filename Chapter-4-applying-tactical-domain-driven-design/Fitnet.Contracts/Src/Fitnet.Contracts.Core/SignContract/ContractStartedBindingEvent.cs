namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract;

using DomainDrivenDesign.BuildingBlocks;

public sealed record ContractStartedBindingEvent(
    Guid Id,
    DateTimeOffset BindingFrom,
    DateTimeOffset? ExpiringAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static ContractStartedBindingEvent Create(DateTimeOffset bindingFrom,
        DateTimeOffset? expiringAt)
        => new(Guid.NewGuid(), bindingFrom, expiringAt, DateTime.Now);
}
