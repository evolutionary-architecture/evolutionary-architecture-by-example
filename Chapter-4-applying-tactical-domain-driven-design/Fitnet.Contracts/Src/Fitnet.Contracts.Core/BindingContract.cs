namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using AddAnnex.BusinessRules;
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

    private BindingContract(
        ContractId contractId,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset expiringAt,
        DateTimeOffset occuredAt)
    {
        Id = BindingContractId.Create();
        ContractId = contractId;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
        BindingFrom = bindingFrom;

        var @event = ContractStartedBindingEvent.Raise(BindingFrom, ExpiringAt, occuredAt);
        RecordEvent(@event);
    }

    internal static BindingContract Start(
        ContractId id,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset expiringAt,
        DateTimeOffset occuredAt) => new(id, customerId, duration, bindingFrom, expiringAt, occuredAt);

    public Annex AddAnnex(DateTimeOffset validFrom, DateTimeOffset now)
    {
        BusinessRuleValidator.Validate(
            new AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(TerminatedAt, ExpiringAt, now));

        return Annex.Add(Id, validFrom, now);
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

public readonly record struct BindingContractId(Guid Value)
{
    internal static BindingContractId Create() => new(Guid.NewGuid());
}

