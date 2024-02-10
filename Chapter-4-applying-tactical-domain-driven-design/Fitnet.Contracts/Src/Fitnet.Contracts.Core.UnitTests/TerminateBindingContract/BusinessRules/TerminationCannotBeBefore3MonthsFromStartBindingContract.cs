namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules;

using Common.Core.BusinessRules;

internal sealed class TerminationCannotBeBefore3MonthsFromStartBindingContract(DateTimeOffset signDateTimeOffset, DateTimeOffset now)
    : IBusinessRule
{
    private readonly TimeSpan _3Months = TimeSpan.FromDays(90);

    public bool IsMet() => now - signDateTimeOffset > _3Months;

    public string Error
    {
        get
        {
            var daysLeft = (_3Months - (now - signDateTimeOffset)).Days;
            var error = $"Termination is not possible until three months have elapsed. {daysLeft} days remaining.";

            return error;
        }
    }
}
