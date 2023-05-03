namespace EvolutionaryArchitecture.Fitnet.Shared.BusinessRulesEngine;

internal class BusinessRuleValidationException : InvalidOperationException
{
    internal BusinessRuleValidationException(string message) : base(message)
    {
    }
}