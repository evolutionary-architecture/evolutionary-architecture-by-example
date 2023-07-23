namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests.Events.EventBus.InMemory;

using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.EventBus.External;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using Infrastructure.Events.EventBus.InMemory;

public sealed class InMemoryEventBusTests : IClassFixture<FitnetWebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public InMemoryEventBusTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory) =>
        _applicationInMemory = applicationInMemoryFactory
            .WithTestExternalEventBus()
            .WithFakeConsumers(Assembly.GetExecutingAssembly());
    
    [Fact]
    public async Task Given_valid_event_published_Then_event_should_be_consumed()
    {
        // Arrange
        var eventBus = _applicationInMemory.Services.GetRequiredService<IInMemoryEventBus>();
        var fakeEvent = FakeEvent.Create();
        
        // Act
        await eventBus.PublishAsync(fakeEvent, CancellationToken.None);
        
        // Assert
        fakeEvent.Consumed.Should().BeTrue();        
    }
}