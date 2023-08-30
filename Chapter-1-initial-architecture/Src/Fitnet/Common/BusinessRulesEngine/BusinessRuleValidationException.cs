namespace EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

internal class BusinessRuleValidationException : InvalidOperationException
{
    internal BusinessRuleValidationException(string message2) : base(message2)
    {
    }
}