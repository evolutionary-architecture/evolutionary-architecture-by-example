namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.BusinessRules;

using Common.Core.BussinessRules;

internal sealed class ContractMustNotBeAlreadySignedRule(bool signed) : IBusinessRule
{
    public bool IsMet() => !signed;
    public Error Error => BusinessRuleError.Create(nameof(ContractMustNotBeAlreadySignedRule), "Contract must not be already signed");
}
