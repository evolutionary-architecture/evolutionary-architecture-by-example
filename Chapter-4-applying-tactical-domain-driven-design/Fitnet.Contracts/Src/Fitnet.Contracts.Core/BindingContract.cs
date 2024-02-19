namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using TerminateContract.BusinessRules;

public sealed class BindingContract
{
    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    public TimeSpan Duration { get; init; }
    public DateTimeOffset TerminationDate { get; private set; }
    public DateTimeOffset BindingFrom { get; init; }


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

    public DateTimeOffset? ExpiringAt { get; set; }

    public void Terminate(DateTimeOffset terminatedAt)
    {
        BusinessRuleValidator.Validate(new TerminationIsNotPossibleUntilThreeMonthsHaveElapsedRule(BindingFrom, terminatedAt));

        TerminationDate = terminatedAt;
    }
}
