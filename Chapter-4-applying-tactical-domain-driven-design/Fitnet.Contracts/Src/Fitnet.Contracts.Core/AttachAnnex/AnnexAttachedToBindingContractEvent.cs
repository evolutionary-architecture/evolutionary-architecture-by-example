﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnex;

using EvolutionaryArchitecture.Fitnet.DomainDrivenDesign.BuildingBlocks;

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
