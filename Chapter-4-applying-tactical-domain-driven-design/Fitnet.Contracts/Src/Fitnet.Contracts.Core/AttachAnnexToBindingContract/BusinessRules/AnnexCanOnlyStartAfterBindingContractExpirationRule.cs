namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnexToBindingContract.BusinessRules;

using Common.Core.BusinessRules;

internal sealed class AnnexCanOnlyStartAfterBindingContractExpirationRule : IBusinessRule
{
    private readonly DateTimeOffset _bindingContractExpiringAt;
    private readonly DateTimeOffset _annexValidFrom;

    internal AnnexCanOnlyStartAfterBindingContractExpirationRule(
        DateTimeOffset bindingContractExpiringAt,
        DateTimeOffset annexValidFrom)
    {
        _bindingContractExpiringAt = bindingContractExpiringAt;
        _annexValidFrom = annexValidFrom;
    }

    public bool IsMet() => _annexValidFrom > _bindingContractExpiringAt;

    public string Error => "Annex can only start after binding contract expiration";
}
