namespace EvolutionaryArchitecture.Fitnet.Common.BussinessRules;

public interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}