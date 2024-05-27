namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.AttachAnnexToBindingContract;

using Api;
using Api.AttachAnnexToBindingContract;
using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using Common.IntegrationTestsToolbox.TestEngine.Time;
using PrepareContract;
using SignContract;
using TerminateBindingContract;

public class AttachAnnexToBindingContractTests(
    FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly FakeTimeProvider FakeSystemTimeProvider = new();

    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
        .WithTestEventBus()
        .WithTime(FakeSystemTimeProvider)
        .CreateClient();

    [Fact]
    internal async Task Given_attach_annex_request_for_active_binding_contract_Then_should_return_created()
    {
        // Arrange
        var contractId = await _applicationHttpClient.PrepareContractAsync();
        var bindingContractId = await _applicationHttpClient.SignContractAsync(contractId);
        var annexesPath = ContractsApiPaths.GetAnnexesPath(bindingContractId);
        AttachAnnexToBindingContractRequest request =
            new AttachAnnexToBindingContractRequestFaker(FakeSystemTimeProvider.GetUtcNow());

        // Act
        var response = await _applicationHttpClient.PostAsJsonAsync(annexesPath, request);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_attach_annex_request_for_non_existing_binding_contract_Then_should_return_not_found()
    {
        // Arrange
        var nonExistingBindingContractId = Guid.NewGuid();
        var annexesPath = ContractsApiPaths.GetAnnexesPath(nonExistingBindingContractId);
        AttachAnnexToBindingContractRequest request =
            new AttachAnnexToBindingContractRequestFaker(FakeSystemTimeProvider.GetUtcNow());

        // Act
        var response = await _applicationHttpClient.PostAsJsonAsync(annexesPath, request);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    internal async Task Given_attach_annex_request_for_terminated_binding_contract_Then_should_return_conflict()
    {
        // Arrange
        var contractId = await _applicationHttpClient.PrepareContractAsync();
        var bindingContractId = await _applicationHttpClient.SignContractAsync(contractId);
        const int timeSkip = 120;
        FakeSystemTimeProvider.SimulateTimeSkip(timeSkip);
        await _applicationHttpClient.TerminateBindingContractAsync(bindingContractId);
        var annexesPath = ContractsApiPaths.GetAnnexesPath(bindingContractId);
        AttachAnnexToBindingContractRequest request =
            new AttachAnnexToBindingContractRequestFaker(FakeSystemTimeProvider.GetUtcNow());

        // Act
        var response = await _applicationHttpClient.PostAsJsonAsync(annexesPath, request);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.Conflict);
    }

    [Fact]
    internal async Task Given_attach_annex_request_for_inactive_binding_contract_Then_should_return_conflict()
    {
        // Arrange
        var contractId = await _applicationHttpClient.PrepareContractAsync();
        var bindingContractId = await _applicationHttpClient.SignContractAsync(contractId);
        var annexesPath = ContractsApiPaths.GetAnnexesPath(bindingContractId);
        const int timeSkip = 367;
        FakeSystemTimeProvider.SimulateTimeSkip(timeSkip);
        AttachAnnexToBindingContractRequest request =
            new AttachAnnexToBindingContractRequestFaker(FakeSystemTimeProvider.GetUtcNow());

        // Act
        var response = await _applicationHttpClient.PostAsJsonAsync(annexesPath, request);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.Conflict);
    }
}
