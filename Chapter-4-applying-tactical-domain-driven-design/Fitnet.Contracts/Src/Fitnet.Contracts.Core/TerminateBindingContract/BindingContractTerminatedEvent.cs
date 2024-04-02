namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.TerminateBindingContract;

using DomainDrivenDesign.BuildingBlocks;

public sealed record BindingContractTerminatedEvent(
    Guid Id,
    DateTimeOffset TerminatedAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static BindingContractTerminatedEvent Raise(DateTimeOffset terminatedAt)
        => new(Guid.NewGuid(), terminatedAt, DateTime.UtcNow);
}
