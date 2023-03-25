namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes.RegisterPass;

public sealed class MarkPassAsExpiredTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly StringContent EmptyContent = new(string.Empty);
    
    private readonly HttpClient _applicationHttpClient;

    public MarkPassAsExpiredTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithConnectionString(ConfigurationKeys.PassesConnectionString, database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_mark_pass_as_expired_request_Then_should_return_created_status_code()
    {
        // Arrange
        var registeredPassPath = await RegisterPass();
        var url = BuildUrl(registeredPassPath);

        // Act
        var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        markAsExpiredResponse.Should().HaveStatusCode(HttpStatusCode.OK);
    }
    
    private async Task<string> RegisterPass()
    {
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker();
        var registerPassResponse = await _applicationHttpClient.PostAsJsonAsync(ApiPaths.Passes, registerPassRequest);
        var path = await registerPassResponse.Content.ReadAsStringAsync();
        
        return path;
    }
    
    private static string BuildUrl(string registeredPassPath) => $"{registeredPassPath}/mark-as-expired";
}