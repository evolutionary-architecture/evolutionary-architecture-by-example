namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.PrepareContract.BusinessRules;

using Common.Core.BusinessRules;

internal sealed class PreviousContractHasToBeSignedRule : IBusinessRule
{
    private readonly bool? _signed;

    internal PreviousContractHasToBeSignedRule(bool? signed) => _signed = signed;

    public bool IsMet() => _signed is true or null;

    public string Error => "Previous contract must be signed by the customer";
}
