namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests.Prepare;

using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.Configuration;
using Common.IntegrationTests.TestEngine.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;
using EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;
using Passes.IntegrationEvents;

public sealed class PrepareOfferTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public PrepareOfferTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithTestEventBus()
            .WithContainerDatabaseConfigured(new OffersDatabaseConfiguration(database.ConnectionString!));

        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    {
        // Arrange
        using var integrationEventHandlerScope = new IntegrationEventHandlerScope<PassExpiredEvent>(_applicationInMemory);
        var @event = PassExpiredEventFaker.CreateValid();

        // Act
        await integrationEventHandlerScope.Consume(@event, CancellationToken.None);

        // Assert
        // EnsureThatOfferPreparedEventWasPublished();
    }

    // private void EnsureThatOfferPreparedEventWasPublished() => _testInMemoryEventBus.Verify(eventBus => eventBus.PublishAsync(It.IsAny<OfferPrepareEvent>(), It.IsAny<CancellationToken>()), Times.Once);
}