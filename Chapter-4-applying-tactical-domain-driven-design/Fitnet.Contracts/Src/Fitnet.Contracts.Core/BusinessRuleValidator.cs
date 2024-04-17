namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using Common.Core.BusinessRules;
using ErrorOr;

public static class BusinessRuleValidator
{
    public static List<Error> Validate(params IBusinessRule[] rules)
    {
        var errors = new List<Error>();
        foreach (var rule in rules)
        {
            if (!rule.IsMet())
            {
                errors.Add(Error.Validation(nameof(rule), rule.Error));
            }
        }

        return errors;
    }
}
