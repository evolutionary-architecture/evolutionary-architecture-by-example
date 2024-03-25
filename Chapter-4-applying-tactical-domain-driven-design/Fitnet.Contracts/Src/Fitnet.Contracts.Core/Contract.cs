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
        TimeSpan duration)
    {
        Id = ContractId.Create();
        CustomerId = customerId;
        PreparedAt = preparedAt;
        Duration = duration;
    }

    public static Contract Prepare(Guid customerId, int customerAge, int customerHeight, DateTimeOffset preparedAt, bool? isPreviousContractSigned = null)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge));
        BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight));
        BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(isPreviousContractSigned));

        var contract = new Contract(customerId, preparedAt, StandardDuration);
        var @event = ContractPreparedEvent.Raise(customerId, preparedAt);
        contract.RecordEvent(@event);

        return contract;
    }

    public BindingContract Sign(DateTimeOffset signedAt, DateTimeOffset today)
    {
        BusinessRuleValidator.Validate(
            new ContractMustNotBeAlreadySigned(IsSigned));

        BusinessRuleValidator.Validate(
            new ContractCanOnlyBeSignedWithin30DaysFromPreparation(PreparedAt, signedAt));

        SignedAt = signedAt;
        ExpiringAt = today.Add(Duration);
        var bindingContract = BindingContract.Start(Id, CustomerId, Duration, today, ExpiringAt.Value);

        return bindingContract;
    }
}
