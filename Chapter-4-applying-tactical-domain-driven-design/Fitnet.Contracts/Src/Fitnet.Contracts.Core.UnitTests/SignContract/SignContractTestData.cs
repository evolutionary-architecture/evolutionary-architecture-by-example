namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

internal sealed class SignContractTestData : TheoryData<DateTimeOffset, DateTimeOffset, DateTimeOffset, DateTimeOffset>
{
    public SignContractTestData()
    {
        AddRow(
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2022, 1, 1, 1, 1, 1, TimeSpan.Zero)
        );
        AddRow(
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 6, 1, 12, 0, 0, TimeSpan.Zero)
        );
        AddRow(
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 2, 15, 8, 30, 0, TimeSpan.Zero)
        );
    }
}
