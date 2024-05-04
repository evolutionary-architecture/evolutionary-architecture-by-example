namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.PrepareContract.BusinessRules;

internal sealed class PreviousContractHasToBeSignedRule : IBusinessRule
{
    private readonly bool? _signed;

    internal PreviousContractHasToBeSignedRule(bool? signed) => _signed = signed;

    public bool IsMet() => _signed is true or null;

    public Error Error => Error.Validation(nameof(PreviousContractHasToBeSignedRule), "Previous contract has to be signed");
}
