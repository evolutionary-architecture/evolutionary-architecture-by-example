namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules;

using Common.Core.BusinessRules;

internal sealed class TerminationCannotBeBefore3MonthsFromStartBindingContract(DateTimeOffset signDateTimeOffset, DateTimeOffset now)
    : IBusinessRule
{
    public bool IsMet() => now - signDateTimeOffset > TimeSpan.FromDays(90);

    public string Error => "";
}
