namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.AddAnnex.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule : IBusinessRule
{
    private readonly DateTimeOffset? _terminatedAt;
    private readonly DateTimeOffset _expiringAt;

    internal AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(
        DateTimeOffset? terminatedAt,
        DateTimeOffset expiringAt)
    {
        _terminatedAt = terminatedAt;
        _expiringAt = expiringAt;
    }

    public bool IsMet() => !_terminatedAt.HasValue && _expiringAt > DateTimeOffset.UtcNow;

    public string Error => "Annex can only be added to active binding contract";
}

