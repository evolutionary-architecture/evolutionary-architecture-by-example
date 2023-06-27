namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests.GenerateNewPassesPerMonthReport;

using Common.Infrastructure.IntegrationTests;
using Common.IntegrationTests.TestEngine.Configuration;
using Contracts.IntegrationEvents;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;
using Reports;
using GenerateNewPassesRegistrationsPerMonthReport.Dtos;
using TestData;

[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;
    
    public GenerateNewPassesPerMonthReportTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ReportsDatabaseConfiguration(database.ConnectionString!))
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
            await RegisterPass(passRegistration.From, passRegistration.To);
    }

    private async Task RegisterPass(DateTimeOffset from, DateTimeOffset to)
    {
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemoryFactory);
        var @event = ContractSignedEventFaker.Create(from, to);
        await integrationEventHandlerScope.Consume(@event, CancellationToken.None);
    }
}