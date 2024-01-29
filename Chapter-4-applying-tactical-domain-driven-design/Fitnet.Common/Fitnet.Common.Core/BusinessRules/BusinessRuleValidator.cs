namespace EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public static class BusinessRuleValidator
{
    public static void Validate(IBusinessRule rule)
    {
        if (!rule.IsMet())
        {
            throw new BusinessRuleValidationException(rule.Error);
        }
    }
}