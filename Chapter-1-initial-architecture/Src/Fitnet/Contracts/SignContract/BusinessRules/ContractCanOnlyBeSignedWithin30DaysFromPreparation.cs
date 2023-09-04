namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract.BusinessRules;

using Common.BusinessRulesEngine;

internal sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparation : IBusinessRule
{
    private readonly DateTimeOffset _preparedAt;
    private readonly DateTimeOffset _signedAt;

    internal ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset preparedAt,
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

    public string Error =>
        "Contract can not be signed because more than 30 days have passed from the contract preparation";
}
