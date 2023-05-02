namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Offers.Prepare;

using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using Fitnet.Offers.Prepare;
using Fitnet.Passes.MarkPassAsExpired.Events;
using Fitnet.Shared.Events.EventBus;

public sealed class PrepareOfferTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly Mock<IEventBus> _fakeEventBus = new();
    private readonly WebApplicationFactory<Program> _applicationInMemory;
    
    public PrepareOfferTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithFakeEventBus(_fakeEventBus)
            .WithContainerDatabaseConfigured(database.ConnectionString!);

        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    {
        // Arrange
        using var integrationEventHandlerScope = new IntegrationEventHandlerScope<PassExpiredEvent>(_applicationInMemory);
        var integrationEventHandler = integrationEventHandlerScope.IntegrationEventHandler;
        var @event = PassExpiredEventFaker.CreateValid();
       
        // Act
        await integrationEventHandler.Handle(@event, CancellationToken.None);
         
        // Assert
        EnsureThatOfferPreparedEventWasPublished();
    }
    
    private void EnsureThatOfferPreparedEventWasPublished() => _fakeEventBus.Verify(eventBus => eventBus.PublishAsync(It.IsAny<OfferPrepareEvent>(), It.IsAny<CancellationToken>()), Times.Once);
}