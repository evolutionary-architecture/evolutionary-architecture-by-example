namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnexToBindingContract.BusinessRules;

internal sealed class AnnexCanOnlyStartDuringBindingContractPeriodRule : IBusinessRule
{
    private readonly DateTimeOffset _bindingContractExpiringAt;
    private readonly DateTimeOffset _annexValidFrom;

    internal AnnexCanOnlyStartDuringBindingContractPeriodRule(
        DateTimeOffset bindingContractExpiringAt,
        DateTimeOffset annexValidFrom)
    {
        _bindingContractExpiringAt = bindingContractExpiringAt;
        _annexValidFrom = annexValidFrom;
    }

    public bool IsMet() => _annexValidFrom <= _bindingContractExpiringAt;

    public Error Error => Error.Validation(nameof(AnnexCanOnlyStartDuringBindingContractPeriodRule), "Annex can only start during binding contract period");
}
