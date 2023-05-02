namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Offers.Prepare;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using Fitnet.Offers.Prepare;
using Fitnet.Passes.MarkPassAsExpired.Events;
using Fitnet.Shared.Events;
using Fitnet.Shared.Events.EventBus;

public sealed class PrepareOfferTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private readonly IntegrationEventHandlerScope _integrationEventHandlerScope;
    private readonly IIntegrationEventHandler<PassExpiredEvent> _integrationEventHandler;

    private readonly Mock<IEventBus> _fakeEventBus = new();

    public PrepareOfferTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        var applicationInMemory = applicationInMemoryFactory
            .WithFakeEventBus(_fakeEventBus)
            .WithContainerDatabaseConfigured(database.ConnectionString!);

        applicationInMemory.CreateClient();
        _integrationEventHandlerScope = new IntegrationEventHandlerScope(applicationInMemory);
        _integrationEventHandler = _integrationEventHandlerScope.GetIntegrationEventHandler<PassExpiredEvent>();
    }

    [Fact]
    internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    {
        // Arrange
        var @event = PassExpiredEventFaker.CreateValid();
       
        // Act
        await _integrationEventHandler.Handle(@event, CancellationToken.None);
         
        // Assert
        EnsureThatOfferPreparedEventWasPublished();
    }
    
    private void EnsureThatOfferPreparedEventWasPublished() => _fakeEventBus.Verify(eventBus => eventBus.PublishAsync(It.IsAny<OfferPrepareEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    
    public async Task InitializeAsync() => 
        await Task.CompletedTask;
    
    public async Task DisposeAsync()
    {
        _integrationEventHandlerScope.Dispose();
        await Task.CompletedTask;
    }
}