namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests.Events.EventBus.InMemory;

using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;

public sealed class InMemoryEventBusTests : IClassFixture<FitnetWebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public InMemoryEventBusTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory) =>
        _applicationInMemory = applicationInMemoryFactory
            .WithFakeConsumers(Assembly.GetExecutingAssembly());
    
    [Fact]
    public async Task Given_valid_event_published_Then_event_should_be_consumed()
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