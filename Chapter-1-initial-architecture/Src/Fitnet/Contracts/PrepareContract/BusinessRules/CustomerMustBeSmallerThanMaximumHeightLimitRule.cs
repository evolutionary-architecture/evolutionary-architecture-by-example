namespace EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract.BusinessRules;

using Common.BusinessRulesEngine;

internal sealed class CustomerMustBeSmallerThanMaximumHeightLimitRule : IBusinessRule
{
    private const int MaximumHeight = 210;

    private readonly int _height;

    internal CustomerMustBeSmallerThanMaximumHeightLimitRule(int height) => _height = height;

    public bool IsMet() => _height <= MaximumHeight;

    public string Error => "Customer height must fit maximum limit for gym instruments";
}
