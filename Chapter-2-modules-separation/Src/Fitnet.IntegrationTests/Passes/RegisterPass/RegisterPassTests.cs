namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using EvolutionaryArchitecture.Fitnet.Passes.Api;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;
using Common.TestEngine.Configuration;

public sealed class RegisterPassTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();

    [Fact]
    internal async Task Given_valid_pass_registration_request_Then_should_return_created_status_code()
    {
        // Arrange
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker();

        // Act
        var registerPassResponse = await _applicationHttpClient.PostAsJsonAsync(PassesApiPaths.Register, registerPassRequest);

        // Assert
        registerPassResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }
}