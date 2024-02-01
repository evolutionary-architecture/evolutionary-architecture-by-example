namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class ContractMustNotBeAlreadySigned(bool signed) : IBusinessRule
{
    public bool IsMet() => signed;

    public string Error => "Contract is already signed";
}
