namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using AttachAnnex.BusinessRules;
using Common.Core.BusinessRules;
using DomainDrivenDesign.BuildingBlocks;
using SignContract;
using TerminateBindingContract;
using TerminateBindingContract.BusinessRules;

public sealed class BindingContract : Entity
{
    public BindingContractId Id { get; init; }
    public ContractId ContractId { get; init; }
    public Guid CustomerId { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTimeOffset TerminatedAt { get; set; }
    public DateTimeOffset BindingFrom { get; init; }
    public DateTimeOffset ExpiringAt { get; init; }
    public ICollection<Annex> AttachedAnnexes { get; }

    private BindingContract(
        ContractId contractId,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset expiringAt)
    {
        Id = BindingContractId.Create();
        ContractId = contractId;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
        BindingFrom = bindingFrom;
        AttachedAnnexes = [];

        var @event = BindingContractStartedEvent.Raise(BindingFrom, ExpiringAt);
        RecordEvent(@event);
    }

    internal static BindingContract Start(
        ContractId id,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset expiringAt) => new(id, customerId, duration, bindingFrom, expiringAt);

    public void AttachAnnex(DateTimeOffset validFrom)
    {
        BusinessRuleValidator.Validate(
            new AnnexCanOnlyBeAttachedToActiveBindingContractRule(TerminatedAt, ExpiringAt, validFrom));

        AttachedAnnexes.Add(Annex.Attach(Id, validFrom));
    }

    public void Terminate(DateTimeOffset terminatedAt)
    {
        BusinessRuleValidator.Validate(
            new TerminationIsPossibleOnlyAfterThreeMonthsHavePassedRule(BindingFrom, terminatedAt));

        TerminatedAt = terminatedAt;

        var @event = BindingContractTerminatedEvent.Raise(TerminatedAt);
        RecordEvent(@event);
    }
}

public record struct ContractId(Guid Value)
{
    internal static ContractId Create() => new(Guid.NewGuid());
}

public readonly record struct BindingContractId(Guid Value)
{
    internal static BindingContractId Create() => new(Guid.NewGuid());
}
