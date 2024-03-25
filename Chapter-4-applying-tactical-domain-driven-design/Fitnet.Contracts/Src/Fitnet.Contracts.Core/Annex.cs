namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using AddAnex;
using DomainDrivenDesign.BuildingBlocks;

public sealed class Annex : Entity
{
    public AnnexId Id { get; init; }
    public BindingContractId BindingContractId { get; init; }
    public DateTimeOffset ValidFrom { get; init; }

    private Annex(BindingContractId bindingContractId, DateTimeOffset validFrom)
    {
        Id = AnnexId.Create();
        BindingContractId = bindingContractId;
        ValidFrom = validFrom;

        var @event = AnnexAddedToBindingContractEvent.Raise(Id, BindingContractId, ValidFrom);
        RecordEvent(@event);
    }

    internal static Annex Add(BindingContractId bindingContractId, DateTimeOffset validFrom) =>
        new(bindingContractId, validFrom);
}

public readonly record struct AnnexId(Guid Value)
{
    internal static AnnexId Create() => new(Guid.NewGuid());
}
