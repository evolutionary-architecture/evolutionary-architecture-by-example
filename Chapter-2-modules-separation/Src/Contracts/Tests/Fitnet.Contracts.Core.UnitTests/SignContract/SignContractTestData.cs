namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

internal static class SignContractTestData
{
    public static IEnumerable<object[]> GetDates()
    {
        yield return new object[]
        {
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2022, 1, 1, 1, 1, 1, TimeSpan.Zero)
        };
        yield return new object[]
        {
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 6, 1, 12, 0, 0, TimeSpan.Zero)
        };
        yield return new object[]
        {
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 2, 15, 8, 30, 0, TimeSpan.Zero)
        };
    }
}
