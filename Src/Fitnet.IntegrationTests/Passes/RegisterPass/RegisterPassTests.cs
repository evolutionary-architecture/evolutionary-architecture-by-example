namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes.RegisterPass;
using Requests;

public sealed class RegisterPassTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithConnectionString(ConfigurationKeys.PassesConnectionString, database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_pass_registration_request_Then_should_return_created_status_code()
    {
        // Arrange
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker();

        // Act
        var registerPassResponse = await _applicationHttpClient.PostAsJsonAsync(ApiPaths.Passes, registerPassRequest);

        // Assert
        registerPassResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }
}