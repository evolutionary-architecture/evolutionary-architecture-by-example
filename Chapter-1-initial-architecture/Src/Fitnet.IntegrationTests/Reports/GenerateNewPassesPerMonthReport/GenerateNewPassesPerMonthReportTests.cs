namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using Common.TestEngine.Time;
using Fitnet.Contracts.SignContract.Events;
using Fitnet.Reports;
using Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport.Dtos;
using Passes.RegisterPass;
using TestData;

public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private static readonly FakeTimeProvider FakeTimeProvider = new(ReportTestCases.FakeNowDate);
    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .WithTime(FakeTimeProvider);
        _applicationHttpClient = _applicationInMemoryFactory.CreateClient();
    }

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        _applicationHttpClient.Dispose();
        await _applicationInMemoryFactory.DisposeAsync();
    }

    [Theory]
    [ClassData(typeof(ReportTestCases))]
    internal async Task Given_valid_generate_new_report_request_Then_should_return_correct_data(
        List<PassRegistrationDateRange> passRegistrationDateRanges)
    {
        // Arrange
        await RegisterPasses(passRegistrationDateRanges);

        // Act
        using var getReportResult = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport, TestContext.Current.CancellationToken);

        // Assert
        getReportResult.StatusCode.ShouldBe(HttpStatusCode.OK);
        var reportData = await getReportResult.Content.ReadFromJsonAsync<NewPassesRegistrationsPerMonthResponse>(TestContext.Current.CancellationToken);
        await Verify(reportData).UseParameters(passRegistrationDateRanges.Count);
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
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemoryFactory);
        var integrationEventHandler = integrationEventHandlerScope.IntegrationEventHandler;
        var @event = ContractSignedEventFaker.Create(from, to);
        await integrationEventHandler.Handle(@event, CancellationToken.None);
    }
}
