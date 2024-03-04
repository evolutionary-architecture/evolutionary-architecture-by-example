namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using DomainDrivenDesign.BuildingBlocks;
using SignContract;
using TerminateContract;
using TerminateContract.BusinessRules;

public sealed class BindingContract : Entity
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTimeOffset TerminatedAt { get; set; }
    public DateTimeOffset BindingFrom { get; init; }
    public DateTimeOffset? ExpiringAt { get; set; }

    private BindingContract(Guid id,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset? expiringAt)
    {
        Id = id;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
        BindingFrom = bindingFrom;

        var @event = ContractStartedBindingEvent.Create(BindingFrom, ExpiringAt);
        RecordEvent(@event);
    }

    internal static BindingContract Start(Guid id, Guid customerId, TimeSpan duration, DateTimeOffset bindingFrom, DateTimeOffset? expiringAt) =>
        new(id, customerId, duration, bindingFrom, expiringAt);

    public void Terminate(DateTimeOffset terminatedAt)
    {
        BusinessRuleValidator.Validate(new TerminationIsPossibleOnlyAfterThreeMonthsHavePassedRule(BindingFrom, terminatedAt));

        TerminatedAt = terminatedAt;

        var @event = BindingContractTerminatedEvent.Create(TerminatedAt);
        RecordEvent(@event);
    }
}
