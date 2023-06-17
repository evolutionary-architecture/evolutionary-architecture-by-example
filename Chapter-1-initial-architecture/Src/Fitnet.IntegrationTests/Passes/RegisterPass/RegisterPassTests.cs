namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using Fitnet.Contracts.SignContract.Events;
using Fitnet.Passes.RegisterPass.Events;
using Fitnet.Shared.Events.EventBus;

public sealed class RegisterPassTests : IClassFixture<WebApplicationFactory<Program>>, 
    IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;
    private readonly Mock<IEventBus> _fakeEventBus = new();

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
                .WithContainerDatabaseConfigured(database.ConnectionString!)
                .WithFakeEventBus(_fakeEventBus);
        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_contract_signed_event_Then_should_register_pass()
    {
        // Arrange
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemory);
        var @event = ContractSignedEventFaker.Create();

        // Act
        await integrationEventHandlerScope.Consume(@event);

        // Assert
        EnsureThatPassRegisteredEventWasPublished();
    }
    
    private void EnsureThatPassRegisteredEventWasPublished() => _fakeEventBus.Verify(
        eventBus => eventBus.PublishAsync(
            It.IsAny<PassRegisteredEvent>(), 
            It.IsAny<CancellationToken>()),
        Times.Once);
}