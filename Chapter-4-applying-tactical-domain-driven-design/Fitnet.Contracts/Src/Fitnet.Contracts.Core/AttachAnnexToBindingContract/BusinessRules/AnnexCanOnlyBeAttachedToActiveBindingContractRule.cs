namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnexToBindingContract.BusinessRules;

using Common.Core.BussinessRules;

internal sealed class AnnexCanOnlyBeAttachedToActiveBindingContractRule : IBusinessRule
{
    private readonly DateTimeOffset? _bindingContractTerminatedAt;
    private readonly DateTimeOffset _bindingContractExpiringAt;
    private readonly DateTimeOffset _now;

    internal AnnexCanOnlyBeAttachedToActiveBindingContractRule(
        DateTimeOffset? bindingContractTerminatedAt,
        DateTimeOffset bindingContractExpiringAt,
        DateTimeOffset now)
    {
        _bindingContractTerminatedAt = bindingContractTerminatedAt;
        _bindingContractExpiringAt = bindingContractExpiringAt;
        _now = now;
    }

    public bool IsMet() => !_bindingContractTerminatedAt.HasValue && _bindingContractExpiringAt > _now;

    public Error Error => BusinessRuleError.Create(nameof(AnnexCanOnlyBeAttachedToActiveBindingContractRule),
        "Annex can only be attached to active binding contract");
}
