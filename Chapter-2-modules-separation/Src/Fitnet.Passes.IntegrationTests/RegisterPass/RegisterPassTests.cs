namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.RegisterPass;

using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using Api;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

public sealed class RegisterPassTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public RegisterPassTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new PassesDatabaseConfiguration(database.ConnectionString!))
            .CreateClient();

    [Fact]
    internal async Task Given_valid_pass_registration_request_Then_should_return_created_status_code()
    {
        // Arrange
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker();

        // Act
        var registerPassResponse =
            await _applicationHttpClient.PostAsJsonAsync(PassesApiPaths.Register, registerPassRequest);

        // Assert
        registerPassResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }
}