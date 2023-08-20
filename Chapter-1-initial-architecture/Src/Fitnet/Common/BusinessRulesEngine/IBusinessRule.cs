namespace EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

internal interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}