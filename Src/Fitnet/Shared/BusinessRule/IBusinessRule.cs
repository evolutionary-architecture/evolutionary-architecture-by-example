namespace SuperSimpleArchitecture.Fitnet.Shared.BusinessRule;

internal interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}