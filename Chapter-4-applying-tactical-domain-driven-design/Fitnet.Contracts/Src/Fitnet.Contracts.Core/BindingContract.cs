namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using DomainDrivenDesign.BuildingBlocks;
using SignContract;
using TerminateContract;
using TerminateContract.BusinessRules;

public sealed class BindingContract : Entity
{
    public Guid Id { get; init; }
    public Guid ContractId { get; init; }
    public Guid CustomerId { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTimeOffset TerminatedAt { get; set; }
    public DateTimeOffset BindingFrom { get; init; }
    public DateTimeOffset? ExpiringAt { get; set; }

    private BindingContract(
        Guid contractId,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset? expiringAt)
    {
        Id = Guid.NewGuid();
        ContractId = contractId;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
        BindingFrom = bindingFrom;
    }

    internal static BindingContract Start(Guid id, Guid customerId, TimeSpan duration, DateTimeOffset bindingFrom, DateTimeOffset expiringAt)
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
