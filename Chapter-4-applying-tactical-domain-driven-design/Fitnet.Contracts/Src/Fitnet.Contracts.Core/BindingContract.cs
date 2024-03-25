namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

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
    public DateTimeOffset? ExpiringAt { get; set; }

    private BindingContract(
        ContractId contractId,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset? expiringAt)
    {
        Id = BindingContractId.Create();
        ContractId = contractId;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
        BindingFrom = bindingFrom;
    }

    internal static BindingContract Start(ContractId id, Guid customerId, TimeSpan duration, DateTimeOffset bindingFrom, DateTimeOffset expiringAt)
    {
        var bindingContract = new BindingContract(id, customerId, duration, bindingFrom, expiringAt);
        var @event = ContractStartedBindingEvent.Raise(bindingFrom, expiringAt);
        bindingContract.RecordEvent(@event);

        return bindingContract;
    }

    public void Terminate(DateTimeOffset terminatedAt)
    {
        BusinessRuleValidator.Validate(new TerminationIsPossibleOnlyAfterThreeMonthsHavePassedRule(BindingFrom, terminatedAt));

        TerminatedAt = terminatedAt;

        var @event = BindingContractTerminatedEvent.Raise(TerminatedAt);
        RecordEvent(@event);
    }
}
