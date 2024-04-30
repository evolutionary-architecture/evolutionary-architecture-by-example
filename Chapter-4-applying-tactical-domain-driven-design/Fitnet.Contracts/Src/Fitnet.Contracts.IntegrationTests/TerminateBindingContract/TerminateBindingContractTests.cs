namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.TerminateBindingContract;

using Api;
using Common.IntegrationTestsToolbox.TestEngine.Time;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using PrepareContract;
using SignContract;

public sealed class TerminateBindingContractTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory, DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly FakeTimeProvider FakeSystemTimeProvider = new(null);
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
            .WithTestEventBus()
            .WithTime(FakeSystemTimeProvider)
            .CreateClient();

    private const int TimeSkip = 120;

    [Fact]
    internal async Task Given_termination_request_for_non_existing_contract_Then_should_return_not_found()
    {
        // Arrange
        var nonExistingContractId = Guid.NewGuid();
        var path = GetUrl(nonExistingContractId);

        // Act
        var response = await _applicationHttpClient.PatchAsync(path, content: null);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    internal async Task Given_contract_termination_request_When_business_rule_is_broken_Then_should_return_conflict()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var path = GetUrl(preparedContractId);
        await _applicationHttpClient.SignContractAsync(preparedContractId);

        // Act
        var terminateContractResponse =
            await _applicationHttpClient.PatchAsync(path, null);

        // Assert
        terminateContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);
    }

    [Fact]
    internal async Task Given_valid_contract_termination_request_Then_should_return_no_content_status_code()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var path = GetUrl(preparedContractId);
        await _applicationHttpClient.SignContractAsync(preparedContractId);
        FakeSystemTimeProvider.SimulateTimeSkip(TimeSkip);

        // Act
        var terminateContractResponse =
            await _applicationHttpClient.PatchAsync(path, null);

        // Assert
        terminateContractResponse.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    private static string GetUrl(Guid bindingContractId) => ContractsApiPaths.Terminate.Replace("{id}", bindingContractId.ToString());
}
