namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Offers.Preapare;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using Fitnet.Passes.MarkPassAsExpired.Events;

public sealed class PrepareOfferTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public PrepareOfferTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!);

        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    {
        // Arrange
        var @event = PassExpiredEventFaker.CreateValid();
        var integrationEventHandler = _applicationInMemory.GetIntegrationEventHandler<PassExpiredEvent>();
        
        // Act
        await integrationEventHandler.Handle(@event, CancellationToken.None);

        // Assert
        // TODO: Assert that offer prepared event was published
    }
}