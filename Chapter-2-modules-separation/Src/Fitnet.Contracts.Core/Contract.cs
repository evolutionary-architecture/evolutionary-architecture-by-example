namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using PrepareContract.BusinessRules;
using SignContract.BusinessRules;

public sealed class Contract
{
    public Guid Id { get; init; }
    
    public Guid CustomerId { get; init; }

    public DateTimeOffset PreparedAt { get; init; }
    
    public DateTimeOffset? SignedAt { get; private set; }
    public bool Signed => SignedAt is not null;

    private Contract(Guid id, 
        Guid customerId, 
        DateTimeOffset preparedAt)
    {
        Id = id;
        CustomerId = customerId;
        PreparedAt = preparedAt;
    }

    public static Contract Prepare(Guid customerId, int customerAge, int customerHeight, DateTimeOffset preparedAt, bool? isPreviousContractSigned = null)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge));
        BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight));
        BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(isPreviousContractSigned));

        return new(Guid.NewGuid(), 
            customerId,
            preparedAt);
    }

    public void Sign(DateTimeOffset signedAt)
    {
        BusinessRuleValidator.Validate(
            new ContractCanOnlyBeSignedWithin30DaysFromPreparation(PreparedAt, signedAt));

        SignedAt = signedAt;
    }
}