namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AddAnnex;

using DomainDrivenDesign.BuildingBlocks;

public sealed record AttachedAnnexToBindingContractEvent(
    Guid Id,
    AnnexId AnnexId,
    BindingContractId BindingContractId,
    DateTimeOffset ValidFrom,
    DateTime OccuredAt) : IDomainEvent
{
    internal static AttachedAnnexToBindingContractEvent Raise(
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
