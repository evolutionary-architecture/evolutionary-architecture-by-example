namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using AddAnex;
using DomainDrivenDesign.BuildingBlocks;

public sealed class Annex : Entity
{
    public AnnexId Id { get; init; }
    public BindingContractId BindingContractId { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTimeOffset ValidFrom { get; init; }
    public DateTimeOffset ExpiringAt { get; set; }

    private Annex(
        BindingContractId bindingContractId,
        TimeSpan duration,
        DateTimeOffset validFrom,
        DateTimeOffset expiringAt)
    {
        Id = AnnexId.Create();
        BindingContractId = bindingContractId;
        Duration = duration;
        ValidFrom = validFrom;
        ExpiringAt = expiringAt;
    }

    internal static Annex Add(
        BindingContractId bindingContractId,
        TimeSpan duration,
        DateTimeOffset validFrom,
        DateTimeOffset expiringAt)
    {
        var annex = new Annex(bindingContractId, duration, validFrom, expiringAt);
        var @event = AnnexAddedToBindingContractEvent.Raise(annex.Id, bindingContractId, validFrom, expiringAt);
        annex.RecordEvent(@event);

        return annex;
    }
}

public readonly record struct AnnexId(Guid Value)
{
    internal static AnnexId Create() => new(Guid.NewGuid());
}
