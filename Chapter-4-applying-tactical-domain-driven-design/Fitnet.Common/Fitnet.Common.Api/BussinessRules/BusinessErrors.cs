namespace EvolutionaryArchitecture.Fitnet.Common.Api.BussinessRules;

using ErrorOr;

public static class BusinessRuleError
{
    public const int Type = 100;
    public static Error Create(string code, string description) => Error.Custom(Type, code, description);
}
