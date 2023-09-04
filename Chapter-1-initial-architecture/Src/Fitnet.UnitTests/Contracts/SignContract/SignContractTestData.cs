namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.SignContract;

internal sealed class SignContractTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2021, 1, 1, 1, 1, 1, TimeSpan.Zero),
            new DateTimeOffset(2022, 1, 1, 1, 1, 1, TimeSpan.Zero)
        },
        new object[]
        {
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2022, 6, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 6, 1, 12, 0, 0, TimeSpan.Zero)
        },
        new object[]
        {
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2023, 2, 15, 8, 30, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 2, 15, 8, 30, 0, TimeSpan.Zero)
        }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
