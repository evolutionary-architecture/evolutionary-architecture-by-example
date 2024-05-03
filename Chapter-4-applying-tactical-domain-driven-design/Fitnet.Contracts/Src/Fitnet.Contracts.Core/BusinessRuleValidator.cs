namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using ErrorOr;

public static class BusinessRuleValidator
{
    public static ErrorOr<Success> Validate(params IBusinessRule[] rules) =>
        rules
            .Where(rule => !rule.IsMet())
            .Select(rule => rule.Error)
            .ToList();
}

public interface IBusinessRule
{
    bool IsMet();

    Error Error { get; }
}
