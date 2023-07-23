namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.RegisterPass;

using Common.Infrastructure.Events.EventBus.InMemory;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;
using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.EventBus.External;
using Common.IntegrationTests.TestEngine.EventBus.InMemory;
using Contracts.IntegrationEvents;

public sealed class RegisterPassTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly Mock<IInMemoryEventBus> _testInMemoryEventBus = new();
    private readonly ITestHarness _testExternalEventBus;
    
    public RegisterPassTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
       var applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new PassesDatabaseConfiguration(database.ConnectionString!))
            .WithTestInMemoryEventBus(_testInMemoryEventBus)
            .WithTestExternalEventBus(typeof(ContractSignedEventConsumer));
        applicationInMemory.CreateClient();
        _testExternalEventBus = applicationInMemory.GetTestExternalEventBus();
    }

    [Fact]
    internal async Task Given_contract_signed_event_Then_should_register_pass()
    {
        // Arrange
        var @event = ContractSignedEventFaker.Create();

        // Act
        await _testExternalEventBus.Bus.Publish(@event);

        // Assert
        _testExternalEventBus.EnsureConsumed<ContractSignedEvent>();
        EnsureThatPassExpiredEventWasPublished();
    }
    
    private void EnsureThatPassExpiredEventWasPublished() => 
        _testInMemoryEventBus.Verify(eventBus => eventBus.PublishAsync(It.IsAny<PassRegisteredEvent>(), It.IsAny<CancellationToken>()), Times.Once);
}