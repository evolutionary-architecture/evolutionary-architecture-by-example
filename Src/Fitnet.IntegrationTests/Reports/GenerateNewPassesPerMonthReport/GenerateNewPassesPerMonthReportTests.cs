namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

using QuestPDF.Infrastructure;
using System.Runtime.CompilerServices;
using Fitnet.Reports;
using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes;
using Fitnet.Passes.RegisterPass;


[UsesVerify]
public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();
    
    [ModuleInitializer]
    internal static void Initialize()
    {
        VerifyDocNet.Initialize();
    }
    //
    // [ModuleInitializer]
    // internal static void Initialize() =>
    //     VerifyAspose.Initialize();
    
    [Fact]
    public async Task Given_valid_generate_new_report_request_Then_should_return_correct_pdf_report()
    {
        // Arrange
        await RegisterPass();
        await RegisterPass();
        await RegisterPass();
        await RegisterPass();

        // Act
        var getReportResult = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport);

        // Assert
        getReportResult.Content.Headers.ContentType!.MediaType.Should().Be("application/pdf");
        var fileBytes = await getReportResult.Content.ReadAsByteArrayAsync();
        fileBytes.Should().NotBeNull();
        
        var stream = new MemoryStream(fileBytes);
        await Verify(stream, "pdf");
    }
    
    private async Task<Guid> RegisterPass()
    {
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker(new DateTimeOffset(2023, 1,1,0,0,0,TimeSpan.Zero), new DateTimeOffset(2023, 1,31,0,0,0,TimeSpan.Zero));
        var registerPassResponse = await _applicationHttpClient.PostAsJsonAsync(PassesApiPaths.Register, registerPassRequest);
        var registeredPassId = await registerPassResponse.Content.ReadFromJsonAsync<Guid>();

        return registeredPassId;
    }
}