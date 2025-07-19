namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.Events.EventBus.InMemory;

using Fitnet.Common.Events.EventBus;
using TestEngine.Configuration;

public sealed class InMemoryEventBusTests(
    WebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(database.ConnectionString!)
        .WithFakeConsumers();

    [Fact]
    internal async Task Given_valid_event_published_Then_event_should_be_consumed()
    {
        // Arrange
        var eventBus = GetEventBus();
        var fakeEvent = FakeEvent.Create();

        // Act
        await eventBus!.PublishAsync(fakeEvent, CancellationToken.None);

        // Assert
        fakeEvent.Consumed.ShouldBeTrue();
    }

    private IEventBus GetEventBus() =>
        _applicationInMemory.Services
            .CreateScope()!
            .ServiceProvider
            .GetRequiredService<IEventBus>();
}
