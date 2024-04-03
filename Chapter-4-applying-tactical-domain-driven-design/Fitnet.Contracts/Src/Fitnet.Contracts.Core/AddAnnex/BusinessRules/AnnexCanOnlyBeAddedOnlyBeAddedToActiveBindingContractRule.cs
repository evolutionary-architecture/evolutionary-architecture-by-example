namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AddAnnex.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule : IBusinessRule
{
    private readonly DateTimeOffset? _terminatedAt;
    private readonly DateTimeOffset _expiringAt;
    private readonly DateTimeOffset? _now;

    internal AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(
        DateTimeOffset? terminatedAt,
        DateTimeOffset expiringAt,
        DateTimeOffset now)
    {
        _terminatedAt = terminatedAt;
        _expiringAt = expiringAt;
        _now = now;
    }

    public bool IsMet() => !_terminatedAt.HasValue && _expiringAt > _now;

    public string Error => "Annex can only be added to active binding contract";
}
