namespace SuperSimpleArchitecture.Fitnet.Shared.BusinessRule;

internal class BusinessRuleValidationException : InvalidOperationException
{
    internal BusinessRuleValidationException(string message) : base(message)
    {
    }
}