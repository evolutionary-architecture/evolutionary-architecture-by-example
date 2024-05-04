namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.BusinessRules;

internal sealed class ContractMustNotBeAlreadySignedRule(bool signed) : IBusinessRule
{
    public bool IsMet() => !signed;
    public Error Error => Error.Validation(nameof(ContractMustNotBeAlreadySignedRule), "Contract must not be already signed");
}
