namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.PrepareContract.BusinessRules;

using Common.BussinessRules;

internal sealed class PreviousContractHasToBeSignedRule : IBusinessRule
{
    private readonly bool? _signed;

    internal PreviousContractHasToBeSignedRule(bool? signed) => _signed = signed;

    public bool IsMet() => _signed is true or null;

    public Error Error => BusinessRuleError.Create(nameof(PreviousContractHasToBeSignedRule), "Previous contract has to be signed");
}
