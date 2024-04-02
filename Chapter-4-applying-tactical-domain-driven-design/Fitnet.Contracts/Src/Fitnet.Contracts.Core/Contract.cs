namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using DomainDrivenDesign.BuildingBlocks;
using PrepareContract;
using PrepareContract.BusinessRules;
using SignContract.BusinessRules;

public sealed class Contract : Entity
{
    private static TimeSpan StandardDuration => TimeSpan.FromDays(365);

    public ContractId Id { get; }

    public Guid CustomerId { get; init; }

    public DateTimeOffset PreparedAt { get; init; }
    public TimeSpan Duration { get; init; }

    public DateTimeOffset? SignedAt { get; private set; }
    public DateTimeOffset? ExpiringAt { get; private set; }

    public bool IsSigned => SignedAt.HasValue;

    // EF needs this constructor to create non-primitive types
    private Contract() { }

    private Contract(
        Guid customerId,
        DateTimeOffset preparedAt,
        DateTimeOffset occuredAt,
        TimeSpan duration)
    {
        Id = ContractId.Create();
        CustomerId = customerId;
        PreparedAt = preparedAt;
        Duration = duration;

        var @event = ContractPreparedEvent.Raise(CustomerId, PreparedAt, occuredAt);
        RecordEvent(@event);
    }

    public static Contract Prepare(
        Guid customerId,
        int customerAge,
        int customerHeight,
        DateTimeOffset preparedAt,
        DateTimeOffset occuredAt,
        bool? isPreviousContractSigned = null)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge));
        BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight));
        BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(isPreviousContractSigned));

        return new Contract(customerId, preparedAt, occuredAt, StandardDuration);
    }

    public BindingContract Sign(DateTimeOffset signedAt, DateTimeOffset now)
    {
        BusinessRuleValidator.Validate(
            new ContractMustNotBeAlreadySignedRule(IsSigned));

        BusinessRuleValidator.Validate(
            new ContractCanOnlyBeSignedWithin30DaysFromPreparationRule(PreparedAt, signedAt));

        SignedAt = signedAt;
        ExpiringAt = now.Add(Duration);
        var bindingContract = BindingContract.Start(Id, CustomerId, Duration, now, ExpiringAt.Value);

        return bindingContract;
    }
}

public readonly record struct ContractId(Guid Value)
{
    internal static ContractId Create() => new(Guid.NewGuid());
}
