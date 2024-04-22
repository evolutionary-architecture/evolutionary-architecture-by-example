namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AttachAnnex.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class AnnexCanOnlyBeAttachedToActiveBindingContractRule : IBusinessRule
{
    private readonly DateTimeOffset? _terminatedAt;
    private readonly DateTimeOffset _expiringAt;
    private readonly DateTimeOffset? _now;

    internal AnnexCanOnlyBeAttachedToActiveBindingContractRule(
        DateTimeOffset? terminatedAt,
        DateTimeOffset expiringAt,
        DateTimeOffset now)
    {
        _terminatedAt = terminatedAt;
        _expiringAt = expiringAt;
        _now = now;
    }

    public bool IsMet() => !_terminatedAt.HasValue && _expiringAt > _now;

    public string Error => "Annex can only be attached to active binding contract";
}
