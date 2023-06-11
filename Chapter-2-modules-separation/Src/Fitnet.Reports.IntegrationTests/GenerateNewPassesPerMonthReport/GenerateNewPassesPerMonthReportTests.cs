namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests.GenerateNewPassesPerMonthReport;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using GenerateNewPassesRegistrationsPerMonthReport;
using GenerateNewPassesRegistrationsPerMonthReport.TestData;
using Passes.Api;
using Passes.Api.RegisterPass;
using Reports;
using Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport.Dtos;

[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ReportsDatabaseConfiguration(database.ConnectionString!))
            .SetFakeSystemClock(ReportTestCases.FakeNowDate)
            .CreateClient();

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
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker(from, to);
        var registerPassResponse = await _applicationHttpClient.PostAsJsonAsync(PassesApiPaths.Register, registerPassRequest);
        await registerPassResponse.Content.ReadFromJsonAsync<Guid>();
    }
}