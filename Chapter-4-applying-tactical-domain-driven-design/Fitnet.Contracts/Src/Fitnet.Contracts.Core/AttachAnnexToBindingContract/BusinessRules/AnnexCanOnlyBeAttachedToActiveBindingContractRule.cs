namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnexToBindingContract.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class AnnexCanOnlyBeAttachedToActiveBindingContractRule : IBusinessRule
{
    private readonly DateTimeOffset? _bindingContractTerminatedAt;
    private readonly DateTimeOffset _bindingContractExpiringAt;
    private readonly DateTimeOffset _annexValidFrom;

    internal AnnexCanOnlyBeAttachedToActiveBindingContractRule(
        DateTimeOffset? bindingContractTerminatedAt,
        DateTimeOffset bindingContractExpiringAt,
        DateTimeOffset annexValidFrom)
    {
        _bindingContractTerminatedAt = bindingContractTerminatedAt;
        _bindingContractExpiringAt = bindingContractExpiringAt;
        _annexValidFrom = annexValidFrom;
    }

    public bool IsMet() => !_bindingContractTerminatedAt.HasValue && _bindingContractExpiringAt > _annexValidFrom;

    public string Error => "Annex can only be attached to active binding contract";
}
