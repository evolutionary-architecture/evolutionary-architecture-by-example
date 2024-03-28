namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AddAnnex;

using EvolutionaryArchitecture.Fitnet.DomainDrivenDesign.BuildingBlocks;

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
        DateTimeOffset validFrom)
        => new(
            Guid.NewGuid(),
            annexId,
            bindingContractId,
            validFrom,
            DateTime.UtcNow);
}
