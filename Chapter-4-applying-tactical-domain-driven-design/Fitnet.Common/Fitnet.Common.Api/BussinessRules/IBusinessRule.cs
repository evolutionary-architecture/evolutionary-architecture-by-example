namespace EvolutionaryArchitecture.Fitnet.Common.Api.BussinessRules;

using ErrorOr;

public interface IBusinessRule
{
    bool IsMet();

    Error Error { get; }
}
