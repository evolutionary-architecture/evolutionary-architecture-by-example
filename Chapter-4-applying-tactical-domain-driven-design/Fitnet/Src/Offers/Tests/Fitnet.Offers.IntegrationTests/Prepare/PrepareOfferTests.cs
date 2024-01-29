namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests.Prepare;

using Api.Prepare;
using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using Passes.IntegrationEvents;

public sealed class PrepareOfferTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly ITestHarness _testEventBus;

    public PrepareOfferTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        var applicationInMemory = applicationInMemoryFactory
            .WithTestEventBus(typeof(PassExpiredEventConsumer))
            .WithContainerDatabaseConfigured(new OffersDatabaseConfiguration(database.ConnectionString!));

        applicationInMemory.CreateClient();
        _testEventBus = applicationInMemory.GetTestExternalEventBus();
    }

    [Fact]
    internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    {
        // Arrange
        var @event = PassExpiredEventFaker.CreateValid();

        // Act
        await _testEventBus.Bus.Publish(@event);

        // Assert
        await _testEventBus.WaitToConsumeMessageAsync<PassExpiredEvent>();
        await EnsureThatOfferPreparedEventWasPublished();
    }

    private async Task EnsureThatOfferPreparedEventWasPublished()
    {
        var offerPrepared = await _testEventBus.Published.Any<OfferPrepareEvent>();
        offerPrepared.Should().BeTrue();
    }
}
