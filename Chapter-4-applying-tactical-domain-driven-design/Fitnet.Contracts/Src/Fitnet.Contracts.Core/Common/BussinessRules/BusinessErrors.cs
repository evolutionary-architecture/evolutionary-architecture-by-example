namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.Common.BussinessRules;

public static class BusinessRuleError
{
    public const int Type = 10;
    public static Error Create(string code, string description) => Error.Custom(Type, code, description);
}
