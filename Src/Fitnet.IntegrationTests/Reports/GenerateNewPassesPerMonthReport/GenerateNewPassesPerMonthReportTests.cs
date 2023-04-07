namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

using Fitnet.Reports;
using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes;
using Fitnet.Passes.RegisterPass;
using Fitnet.Reports.GenerateNewPassesPerMonthReport.Dtos;
using TestData;

[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;
    
    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .SetFakeSystemClock(ReportTestCases.FakeNowDate)
            .CreateClient();

    [Theory]
    [ClassData(typeof(ReportTestCases))]
    public async Task Given_valid_generate_new_report_request_Then_should_return_correct_data(List<PassRegistrationDateRange> passRegistrationDateRanges)
    {
        // Arrange
        await RegisterPasses(passRegistrationDateRanges);
        
        // Act
        var getReportResult = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport);

        // Assert
        getReportResult.Should().HaveStatusCode(HttpStatusCode.OK);
        var reportData = await getReportResult.Content.ReadFromJsonAsync<List<NewPassesPerMonthDto>>();
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