namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.RegisterPass;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;
using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.EventBus;
using Contracts.IntegrationEvents;

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
        await EnsureThatPassRegistered();
    }
    
    private async Task EnsureThatPassRegistered()
    {
        var passRegisteredEventPublished = await _testEventBus.Published.Any<PassRegisteredEvent>();
        passRegisteredEventPublished.Should().BeTrue();
    }
}