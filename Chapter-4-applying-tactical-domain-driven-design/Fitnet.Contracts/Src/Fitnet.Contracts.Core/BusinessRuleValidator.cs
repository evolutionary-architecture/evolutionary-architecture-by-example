namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using ErrorOr;

public static class BusinessRuleValidator
{
    public static bool Validate(this IBusinessRule rule) =>
        rule.IsMet();
}

public interface IBusinessRule
{
    bool IsMet();

    static Error Error { get; }
}
