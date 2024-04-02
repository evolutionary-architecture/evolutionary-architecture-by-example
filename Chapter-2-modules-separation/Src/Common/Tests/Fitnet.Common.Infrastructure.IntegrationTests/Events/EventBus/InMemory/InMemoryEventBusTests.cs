namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests.Events.EventBus.InMemory;

using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.Time;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;

public sealed class InMemoryEventBusTests(
    FitnetWebApplicationFactory<Program> applicationInMemoryFactory) : IClassFixture<
    FitnetWebApplicationFactory<Program>>
{
    private static readonly FakeTimeProvider FakeTimeProvider = new();
    private WebApplicationFactory<Program> ApplicationInMemory => applicationInMemoryFactory
        .WithTime(FakeTimeProvider)
        .WithFakeConsumers(Assembly.GetExecutingAssembly());

    [Fact]
    internal async Task Given_valid_event_published_Then_event_should_be_consumed()
    {
        // Arrange
        var eventBus = ApplicationInMemory.Services.GetRequiredService<IEventBus>();
        var fakeEvent = FakeEvent.Create();

        // Act
        await eventBus.PublishAsync(fakeEvent, CancellationToken.None);

        // Assert
        fakeEvent.Consumed.Should().BeTrue();
    }
}
