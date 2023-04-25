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
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var @event = PassExpiredEventFaker.CreateValid();
        var integrationEventHandler = _applicationInMemory.GetIntegrationEventHandler<PassExpiredEvent>();
        
        // Act
        await integrationEventHandler.Handle(@event, CancellationToken.None);

        // Assert
    }
}