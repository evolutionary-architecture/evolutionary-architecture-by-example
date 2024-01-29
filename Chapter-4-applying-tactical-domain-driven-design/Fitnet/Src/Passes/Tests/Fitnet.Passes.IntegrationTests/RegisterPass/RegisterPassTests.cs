namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.RegisterPass;

using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using Contracts.IntegrationEvents;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

public sealed class RegisterPassTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly ITestHarness _testEventBus;

    public RegisterPassTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        var applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new PassesDatabaseConfiguration(database.ConnectionString!))
            .WithTestEventBus(typeof(ContractSignedEventConsumer));
        applicationInMemory.CreateClient();
        _testEventBus = applicationInMemory.GetTestExternalEventBus();
    }

    [Fact]
    internal async Task Given_contract_signed_event_Then_should_register_pass()
    {
        // Arrange
        var @event = ContractSignedEventFaker.Create();

        // Act
        await _testEventBus.Bus.Publish(@event);

        // Assert
        _testEventBus.EnsureConsumed<ContractSignedEvent>();
    }
}
