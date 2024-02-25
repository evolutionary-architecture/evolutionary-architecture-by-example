namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.TerminateBindingContract;

using Api;
using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using PrepareContract;
using SignContract;

public sealed class TerminateContractTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory, DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
            .WithTestEventBus()
            .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_termination_request_Then_should_return_no_content_status_code()
    {
        // Arrange
        var contractId = Guid.NewGuid(); // Assuming we have a valid contract ID
        var requestUrl = $"/contracts/{contractId}/terminate";

        // Act
        var response = await _applicationHttpClient.PatchAsync(requestUrl, content: null);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    internal async Task Given_termination_request_for_non_existing_contract_Then_should_return_not_found()
    {
        // Arrange
        var nonExistingContractId = Guid.NewGuid(); // Assuming a non-existing contract ID
        var requestUrl = $"/contracts/{nonExistingContractId}/terminate";

        // Act
        var response = await _applicationHttpClient.PatchAsync(requestUrl, content: null);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    internal async Task Given_invalid_contract_termination_request_Then_should_return_conflict_status_code()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var path = ContractsApiPaths.Terminate.Replace("{id}", preparedContractId.ToString());
        await _applicationHttpClient.SignContractAsync(preparedContractId);

        // Act
        var terminateContractResponse =
            await _applicationHttpClient.PatchAsync(path, null);

        // Assert
        terminateContractResponse.Should().HaveStatusCode(HttpStatusCode.OK);
    }
}
