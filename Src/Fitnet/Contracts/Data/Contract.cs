using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract.BusinessRules;
using SuperSimpleArchitecture.Fitnet.Shared.BusinessRulesEngine;

namespace SuperSimpleArchitecture.Fitnet.Contracts.Data;

internal sealed class Contract
{
    public Guid Id { get; init; }

    private Contract(Guid id)
    {
        Id = id;
    }

    internal static Contract Prepare(int customerAge, int customerHeight)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge));
        BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight));
        
        return new(Guid.NewGuid());
    }
}