namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.TerminateBindingContract;

using Common.IntegrationTestsToolbox.TestEngine.Time;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using PrepareContract;
using SignContract;

public sealed class TerminateBindingContractTests(
    FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly FakeTimeProvider FakeSystemTimeProvider = new();

    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
        .WithTestEventBus()
        .WithTime(FakeSystemTimeProvider)
        .CreateClient();

    private const int TimeSkip = 120;

    [Fact]
    internal async Task
        Given_binding_contract_termination_request_When_binding_contract_does_not_exist_Then_should_return_not_found()
    {
        // Arrange
        var nonExistingBindingContractId = Guid.NewGuid();
        var request = TerminateBindingContractRequestParameters.GetValid(nonExistingBindingContractId);

        // Act
        var response = await _applicationHttpClient.PatchAsync(request.Url, content: null);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    internal async Task
        Given_binding_contract_termination_request_When_binding_contract_exists_Then_should_return_no_content()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var bindingContractId = await _applicationHttpClient.SignContractAsync(preparedContractId);
        var request = TerminateBindingContractRequestParameters.GetValid(bindingContractId);
        FakeSystemTimeProvider.SimulateTimeSkip(TimeSkip);

        // Act
        var terminateContractResponse =
            await _applicationHttpClient.PatchAsync(request.Url, null);

        // Assert
        terminateContractResponse.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    internal async Task
        Given_binding_contract_termination_request_When_three_months_from_contract_signing_passed_Then_should_return_conflict()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var bindingContractId = await _applicationHttpClient.SignContractAsync(preparedContractId);
        var request = TerminateBindingContractRequestParameters.GetValid(bindingContractId);

        // Act
        var terminateContractResponse =
            await _applicationHttpClient.PatchAsync(request.Url, null);

        // Assert
        terminateContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);
    }
}
