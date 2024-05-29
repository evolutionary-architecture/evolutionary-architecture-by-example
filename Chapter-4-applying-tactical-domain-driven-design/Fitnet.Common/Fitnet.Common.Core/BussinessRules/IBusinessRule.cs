namespace EvolutionaryArchitecture.Fitnet.Common.Core.BussinessRules;

using ErrorOr;

public interface IBusinessRule
{
    bool IsMet();

    Error Error { get; }
}
