namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using TerminateContract.BusinessRules;

public sealed class BindingContract : Entity
{
    public Guid Id { get; init; }
    private Guid CustomerId { get; init; }
    private TimeSpan Duration { get; init; }
    private DateTimeOffset TerminatedAt { get; set; }
    private DateTimeOffset BindingFrom { get; init; }
    private DateTimeOffset? ExpiringAt { get; set; }

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
    }

    internal static BindingContract Start(Guid id, Guid customerId, TimeSpan duration, DateTimeOffset bindingFrom, DateTimeOffset? expiringAt) =>
        new(id, customerId, duration, bindingFrom, expiringAt);

    public void Terminate(DateTimeOffset terminatedAt)
    {
        BusinessRuleValidator.Validate(new TerminationIsPossibleOnlyAfterThreeMonthsHavePassedRule(BindingFrom, terminatedAt));

        TerminatedAt = terminatedAt;
    }
}

public record BindinigContractTerminated(DateTimeOffset TerminatedAt);
public record ContractStartedBinding(DateTimeOffset BindingFrom);
