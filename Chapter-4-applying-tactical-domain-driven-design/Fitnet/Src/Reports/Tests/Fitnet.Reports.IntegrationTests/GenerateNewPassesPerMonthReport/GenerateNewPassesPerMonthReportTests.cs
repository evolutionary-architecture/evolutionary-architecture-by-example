namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests.GenerateNewPassesPerMonthReport;

using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using Common.IntegrationTestsToolbox.TestEngine.Time;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using GenerateNewPassesRegistrationsPerMonthReport.Dtos;
using Passes.Api.RegisterPass;
using Contracts.IntegrationEvents;
using TestData;

[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly FakeTimeProvider FakeTimeProvider = new(ReportTestCases.FakeNowDate);
    private readonly HttpClient _applicationHttpClient;
    private readonly ITestHarness _testExternalEventBus;

    public GenerateNewPassesPerMonthReportTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        var applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ReportsDatabaseConfiguration(database.ConnectionString!))
            .WithTestEventBus(typeof(ContractSignedEventConsumer))
            .WithTime(FakeTimeProvider);
        _applicationHttpClient = applicationInMemory.CreateClient();
        _testExternalEventBus = applicationInMemory.GetTestExternalEventBus();
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

        await _testExternalEventBus.WaitToConsumeMessagesAllAsync<ContractSignedEvent>(reportTestData.Count);
    }

    private async Task RegisterPass(DateTimeOffset from, DateTimeOffset to)
    {
        var @event = ContractSignedEventFaker.Create(from, to);
        await _testExternalEventBus.Bus.Publish(@event);
    }
}
