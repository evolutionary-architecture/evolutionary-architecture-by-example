namespace EvolutionaryArchitecture.Fitnet.Common.Core.BussinessRules;

public class BusinessRuleValidationException : InvalidOperationException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}