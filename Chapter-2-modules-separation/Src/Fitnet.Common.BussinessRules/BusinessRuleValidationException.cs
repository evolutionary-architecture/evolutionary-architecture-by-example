namespace EvolutionaryArchitecture.Fitnet.Common.BussinessRules;

public class BusinessRuleValidationException : InvalidOperationException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}