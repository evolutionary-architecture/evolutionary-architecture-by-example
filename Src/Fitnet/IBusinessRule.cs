namespace SuperSimpleArchitecture.Fitnet;

public interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}