namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.Common.BussinessRules;

public interface IBusinessRule
{
    bool IsMet();

    Error Error { get; }
}
