namespace SuperSimpleArchitecture.Fitnet.Shared.BusinessRule;

public interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}