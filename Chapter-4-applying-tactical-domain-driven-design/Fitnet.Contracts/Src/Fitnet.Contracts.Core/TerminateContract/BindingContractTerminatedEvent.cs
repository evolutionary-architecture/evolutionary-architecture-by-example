namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.TerminateContract;

using DomainDrivenDesign.BuildingBlocks;

public sealed record BindingContractTerminatedEvent(
    Guid Id,
    DateTimeOffset TerminatedAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static BindingContractTerminatedEvent Create(DateTimeOffset terminatedAt)
        => new(Guid.NewGuid(), terminatedAt, DateTime.Now);
}
