namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

using Common.TestEngine.Configuration;
using Fitnet.Common.Events.EventBus;
using Fitnet.Reports;
using Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport.Dtos;
using Passes.RegisterPass;
using TestData;

[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .SetFakeSystemClock(ReportTestCases.FakeNowDate);

        _applicationHttpClient = _applicationInMemoryFactory.CreateClient();
    }


    [Theory]
    [ClassData(typeof(ReportTestCases))]
    internal async Task Given_valid_generate_new_report_request_Then_should_return_correct_data(List<PassRegistrationDateRange> passRegistrationDateRanges)
    {
        // Arrange
        await RegisterPasses(passRegistrationDateRanges);

        // Act
        var getReportResult = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport);

        // Assert
        getReportResult.Should().HaveStatusCode(HttpStatusCode.OK);
        var reportData = await getReportResult.Content.ReadFromJsonAsync<NewPassesRegistrationsPerMonthResponse>();
        await Verify(reportData);
    }

    private async Task RegisterPasses(List<PassRegistrationDateRange> reportTestData)
    {
        foreach (var passRegistration in reportTestData)
        {
            await RegisterPass(passRegistration.From, passRegistration.To);
        }
    }

    private async Task RegisterPass(DateTimeOffset from, DateTimeOffset to)
    {
        using var serviceScope = _applicationInMemoryFactory.Services.CreateScope();
        var eventBus = serviceScope.ServiceProvider.GetRequiredService<IEventBus>();
        var @event = ContractSignedEventFaker.Create(from, to);

        // Act
        await eventBus.PublishAsync(@event);
    }
}
