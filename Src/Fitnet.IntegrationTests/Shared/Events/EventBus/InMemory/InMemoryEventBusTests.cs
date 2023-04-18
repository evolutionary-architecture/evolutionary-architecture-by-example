namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Shared.Events.EventBus.InMemory;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Shared.Events.EventBus;

public sealed class InMemoryEventBusTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public InMemoryEventBusTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .WithFakeConsumers();
    
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