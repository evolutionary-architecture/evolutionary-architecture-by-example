namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests.Prepare;

using Common.Infrastructure.Events.EventBus;
using Common.IntegrationTests.TestEngine;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;
using EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;
using Passes.IntegrationEvents;

public sealed class PrepareOfferTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly IEventBus _fakeEventBus = Substitute.For<IEventBus>();
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public PrepareOfferTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithFakeEventBus(_fakeEventBus)
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
        EnsureThatOfferPreparedEventWasPublished();
    }

    private void EnsureThatOfferPreparedEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<OfferPrepareEvent>(), Arg.Any<CancellationToken>());
}
