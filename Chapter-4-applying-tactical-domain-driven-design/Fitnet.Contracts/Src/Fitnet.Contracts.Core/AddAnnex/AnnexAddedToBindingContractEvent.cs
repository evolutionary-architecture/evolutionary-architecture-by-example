namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AddAnnex;

using DomainDrivenDesign.BuildingBlocks;

public sealed record AnnexAddedToBindingContractEvent(
    Guid Id,
    AnnexId AnnexId,
    BindingContractId BindingContractId,
    DateTimeOffset ValidFrom,
    DateTime OccuredAt) : IDomainEvent
{
    internal static AnnexAddedToBindingContractEvent Raise(
        AnnexId annexId,
        BindingContractId bindingContractId,
        DateTimeOffset validFrom,
        DateTimeOffset occuredAt)
        => new(
            Guid.NewGuid(),
            annexId,
            bindingContractId,
            validFrom,
            occuredAt.DateTime);
}
