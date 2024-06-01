namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.BusinessRules;

using Common.Core.BussinessRules;

internal sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparationRule : IBusinessRule
{
    private readonly DateTimeOffset _preparedAt;
    private readonly DateTimeOffset _signedAt;

    internal ContractCanOnlyBeSignedWithin30DaysFromPreparationRule(DateTimeOffset preparedAt,
        DateTimeOffset signedAt)
    {
        _preparedAt = preparedAt;
        _signedAt = signedAt;
    }

    public bool IsMet()
    {
        var timeDifference = _signedAt.Date - _preparedAt.Date;

        return timeDifference <= TimeSpan.FromDays(30);
    }

    public Error Error => BusinessRuleError.Create(nameof(ContractCanOnlyBeSignedWithin30DaysFromPreparationRule),
        "Contract can only be signed within 30 days from preparation");
}
