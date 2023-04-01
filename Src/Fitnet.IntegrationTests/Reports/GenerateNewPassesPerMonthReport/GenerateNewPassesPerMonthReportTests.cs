namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

using Fitnet.Reports;
using Common.TestEngine;
using Common.TestEngine.Configuration;

[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_generate_new_report_request_Then_should_return_correct_pdf_report()
    {
        // Arrange
        
        // Act
        var getReportResult = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport);

        // Assert
        getReportResult.Content.Headers.ContentType!.MediaType.Should().Be("application/pdf");
        var fileBytes = await getReportResult.Content.ReadAsStreamAsync();
        fileBytes.Should().NotBeNull();
    }
}