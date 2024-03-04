namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using DomainDrivenDesign.BuildingBlocks;
using PrepareContract;
using PrepareContract.BusinessRules;
using SignContract.BusinessRules;

public sealed class Contract : Entity
{
    private static TimeSpan StandardDuration => TimeSpan.FromDays(365);

    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    public DateTimeOffset PreparedAt { get; init; }
    public TimeSpan Duration { get; init; }

    public DateTimeOffset? SignedAt { get; private set; }
    public DateTimeOffset? ExpiringAt { get; private set; }

    public bool IsSigned => SignedAt.HasValue;

    private Contract(Guid id,
        Guid customerId,
        DateTimeOffset preparedAt,
        TimeSpan duration)
    {
        Id = id;
        CustomerId = customerId;
        PreparedAt = preparedAt;
        Duration = duration;

        var @event = ContractPreparedEvent.Create(customerId, PreparedAt);
        RecordEvent(@event);
    }

    public static Contract Prepare(Guid customerId, int customerAge, int customerHeight, DateTimeOffset preparedAt, bool? isPreviousContractSigned = null)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge));
        BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight));
        BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(isPreviousContractSigned));

        return new(Guid.NewGuid(),
            customerId,
            preparedAt,
            StandardDuration);
    }

    public BindingContract Sign(DateTimeOffset signedAt, DateTimeOffset today)
    {
        BusinessRuleValidator.Validate(
            new ContractMustNotBeAlreadySigned(IsSigned));

        BusinessRuleValidator.Validate(
            new ContractCanOnlyBeSignedWithin30DaysFromPreparation(PreparedAt, signedAt));

        SignedAt = signedAt;
        ExpiringAt = today.Add(Duration);
        var bindingContract = BindingContract.Start(Id, CustomerId, Duration, today, ExpiringAt);

        return bindingContract;
    }
}
