namespace EvolutionaryArchitecture.Fitnet.Common.Core.BussinessRules;

public interface IBusinessRule
{
    bool IsMet();

    Error Error { get; }
}
