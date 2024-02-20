namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules.TerminationIsPossibleOnlyAfterThreeMonthsHavePassed.TestData;

internal sealed class BindingContractThreeMonthsHaveNotElapsedTestData : TheoryData<DateTimeOffset, DateTimeOffset>
{
    public BindingContractThreeMonthsHaveNotElapsedTestData()
    {
        AddRow(
            new DateTimeOffset(2022, 3, 4, 5, 1, 3, TimeSpan.Zero),
            new DateTimeOffset(2022, 5, 10, 5, 1, 3, TimeSpan.Zero)
        );
        AddRow(
            new DateTimeOffset(2022, 6, 15, 10, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 7, 15, 10, 30, 0, TimeSpan.Zero)
        );
        AddRow(
            new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
        );
    }
}
