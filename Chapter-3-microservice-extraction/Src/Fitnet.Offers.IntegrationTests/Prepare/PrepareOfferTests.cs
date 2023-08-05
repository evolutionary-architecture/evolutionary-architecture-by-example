namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests.Prepare;

using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;

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
    //
    // [Fact]
    // internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    // {
    //     // Arrange
    //     using var integrationEventHandlerScope = new IntegrationEventHandlerScope<PassExpiredEvent>(_applicationInMemory);
    //     var @event = PassExpiredEventFaker.CreateValid();
    //     
    //     // Act
    //     await integrationEventHandlerScope.Consume(@event, CancellationToken.None);
    //
    //     // Assert
    //     EnsureThatOfferPreparedEventWasPublished();
    // }

    // private void EnsureThatOfferPreparedEventWasPublished() => _testInMemoryEventBus.Verify(eventBus => eventBus.PublishAsync(It.IsAny<OfferPrepareEvent>(), It.IsAny<CancellationToken>()), Times.Once);
}