namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BussinessRules;
using PrepareContract.BusinessRules;
using SignContract.BusinessRules;

public sealed class Contract
{
    public Guid Id { get; init; }
    
    public DateTimeOffset PreparedAt { get; init; }
    
    public DateTimeOffset SignedAt { get; private set; }

    private Contract(Guid id, DateTimeOffset preparedAt)
    {
        Id = id;
        PreparedAt = preparedAt;
    }

    public static Contract Prepare(int customerAge, int customerHeight, DateTimeOffset preparedAt)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge));
        BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight));
        
        return new(Guid.NewGuid(), preparedAt);
    }

    public void Sign(DateTimeOffset signedAt)
    {
        BusinessRuleValidator.Validate(
            new ContractCanOnlyBeSignedWithin30DaysFromPreparation(PreparedAt, signedAt));

        SignedAt = signedAt;
    }
}