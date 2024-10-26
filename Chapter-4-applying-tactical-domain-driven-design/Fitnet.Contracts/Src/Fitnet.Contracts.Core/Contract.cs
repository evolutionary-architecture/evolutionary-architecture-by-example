namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BussinessRules;
using DomainDrivenDesign.BuildingBlocks;
using PrepareContract;
using PrepareContract.BusinessRules;
using SignContract.BusinessRules;
using SignContract.Signatures;

public sealed class Contract : Entity
{
    private static TimeSpan StandardDuration => TimeSpan.FromDays(365);

    public ContractId Id { get; }

    public Guid CustomerId { get; init; }

    public DateTimeOffset PreparedAt { get; init; }
    public TimeSpan Duration { get; init; }

    public DigitalSignature? Signature { get; private set; }
    public DateTimeOffset? ExpiringAt { get; private set; }

    public bool IsSigned => Signature is not null;

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

        var @event = ContractPreparedEvent.Raise(CustomerId, PreparedAt);
        RecordEvent(@event);
    }

    public static ErrorOr<Contract> Prepare(
        Guid customerId,
        int customerAge,
        int customerHeight,
        DateTimeOffset preparedAt,
        bool? isPreviousContractSigned = null) =>
        BusinessRuleValidator.Validate(
            new ContractCanBePreparedOnlyForAdultRule(customerAge),
            new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight),
            new PreviousContractHasToBeSignedRule(isPreviousContractSigned))
            .Then<Contract>(_ => new Contract(customerId, preparedAt, StandardDuration));

    public ErrorOr<BindingContract> Sign(DigitalSignature digitalSignature, DateTimeOffset now) =>
        BusinessRuleValidator.Validate(
                new ContractMustNotBeAlreadySignedRule(IsSigned),
                new ContractCanOnlyBeSignedWithin30DaysFromPreparationRule(PreparedAt, digitalSignature.Date))
            .Then(_ =>
            {
                Signature = digitalSignature;
                ExpiringAt = now.Add(Duration);
                var bindingContract = BindingContract.Start(Id, CustomerId, Duration, now, ExpiringAt.Value);

                return bindingContract;
            });
}
