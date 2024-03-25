namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AddAnex;

using DomainDrivenDesign.BuildingBlocks;

public sealed record AnnexAddedToBindingContractEvent(
    Guid Id,
    AnnexId AnnexId,
    BindingContractId BindingContractId,
    DateTimeOffset ValidFrom,
    DateTimeOffset ExpiringAt,
    DateTime OccuredAt) : IDomainEvent
{
    internal static AnnexAddedToBindingContractEvent Raise(
        AnnexId annexId,
        BindingContractId bindingContractId,
        DateTimeOffset validFrom,
        DateTimeOffset expiringAt)
        => new(
            Guid.NewGuid(),
            annexId,
            bindingContractId,
            validFrom,
            expiringAt,
            DateTime.UtcNow);
}
