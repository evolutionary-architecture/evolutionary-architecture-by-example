namespace SuperSimpleArchitecture.Fitnet;

public class BusinessRuleValidationException : InvalidOperationException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}