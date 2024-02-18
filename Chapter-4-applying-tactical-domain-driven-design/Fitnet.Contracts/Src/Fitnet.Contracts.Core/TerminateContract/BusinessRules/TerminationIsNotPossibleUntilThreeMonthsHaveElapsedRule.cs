namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.TerminateContract.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class TerminationIsNotPossibleUntilThreeMonthsHaveElapsedRule(DateTimeOffset signDateTimeOffset, DateTimeOffset now)
    : IBusinessRule
{
    private const int ThreeMonths = 3;

    public bool IsMet()
    {
        var threeMonthsFromSignDate = signDateTimeOffset.AddMonths(ThreeMonths);

        return now >= threeMonthsFromSignDate;
    }

    public string Error
    {
        get
        {
            var threeMonthsFromSignDate = signDateTimeOffset.AddMonths(ThreeMonths);
            var daysRemaining = (threeMonthsFromSignDate - now).Days;

            var error = $"Termination is not possible until three months have elapsed. {daysRemaining} days remaining.";

            return error;
        }
    }
}
