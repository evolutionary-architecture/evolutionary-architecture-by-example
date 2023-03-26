namespace SuperSimpleArchitecture.Fitnet.Shared.BusinessRule;

public class BusinessRuleValidationException : InvalidOperationException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}