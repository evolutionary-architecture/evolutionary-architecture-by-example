﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules.BindingContract3MonthsNotPassed.TestData;

internal sealed class BindingContract3MonthsPassedTestData : TheoryData<DateTimeOffset, DateTimeOffset>
{
    public BindingContract3MonthsPassedTestData()
    {
        AddRow(
            new DateTimeOffset(2022, 3, 4, 5, 1, 3, TimeSpan.Zero),
            new DateTimeOffset(2022, 9, 10, 5, 1, 3, TimeSpan.Zero)
        );
        AddRow(
            new DateTimeOffset(2022, 6, 15, 10, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 9, 15, 10, 30, 0, TimeSpan.Zero)
        );
        AddRow(
            new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 5, 1, 0, 0, 0, TimeSpan.Zero)
        );
    }
}
