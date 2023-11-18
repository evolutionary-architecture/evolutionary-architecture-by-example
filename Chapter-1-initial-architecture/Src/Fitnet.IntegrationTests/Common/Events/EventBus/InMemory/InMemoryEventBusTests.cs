namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.Events.EventBus.InMemory;

using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

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
        var eventBus = _applicationInMemory.Services.GetRequiredService<IEventBus>();
        var fakeEvent = FakeEvent.Create();

        // Act
        await eventBus.PublishAsync(fakeEvent, CancellationToken.None);

        // Assert
        fakeEvent.Consumed.Should().BeTrue();
    }
}
