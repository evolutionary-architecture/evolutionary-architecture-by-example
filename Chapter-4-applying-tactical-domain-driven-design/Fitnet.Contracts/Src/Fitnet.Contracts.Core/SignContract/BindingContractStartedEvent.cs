namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract;

using DomainDrivenDesign.BuildingBlocks;

public sealed record BindingContractStartedEvent(
    Guid Id,
    DateTimeOffset BindingFrom,
    DateTimeOffset? ExpiringAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static BindingContractStartedEvent Raise(
        DateTimeOffset bindingFrom,
        DateTimeOffset? expiringAt)
        => new(Guid.NewGuid(), bindingFrom, expiringAt, DateTime.UtcNow);
}
