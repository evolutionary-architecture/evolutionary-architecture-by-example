namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnexToBindingContract;

using DomainDrivenDesign.BuildingBlocks;

public sealed record AnnexAttachedToBindingContractEvent(
    Guid Id,
    AnnexId AnnexId,
    BindingContractId BindingContractId,
    DateTimeOffset ValidFrom,
    DateTime OccuredAt) : IDomainEvent
{
    internal static AnnexAttachedToBindingContractEvent Raise(
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
