namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport.TestData;

internal sealed class ReportTestCases : TheoryData<List<PassRegistrationDateRange>>
{
    internal static DateTimeOffset FakeNowDate = new(2021, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public ReportTestCases() => Add(new List<PassRegistrationDateRange>
        {
            new(new DateTimeOffset(FakeNowDate.Year, 1, 3, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 1, 10 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 1, 5, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 1, 20 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 2, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 2, 28 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 3, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 3, 31 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 4, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 4, 30 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 5, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 5, 31 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 6, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 6, 30 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 7, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 7, 31 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 8, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 8, 31 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 9, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 9, 30 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 10, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 10, 31 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 11, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 11, 30 ,1, 1, 1,1, TimeSpan.Zero)),
            new(new DateTimeOffset(FakeNowDate.Year, 12, 1, 1, 1,1, TimeSpan.Zero), new DateTimeOffset(FakeNowDate.Year, 12, 31 ,1, 1, 1,1, TimeSpan.Zero))
        });
}
