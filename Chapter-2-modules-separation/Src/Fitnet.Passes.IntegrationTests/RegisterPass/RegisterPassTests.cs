namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.RegisterPass;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;
using Common.Infrastructure.Events.EventBus;
using Common.Infrastructure.IntegrationTests;
using Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;
using Contracts.Application.Sign;

public sealed class RegisterPassTests : IClassFixture<FitnetWebApplicationFactory<Program>>, 
    IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;
    private readonly Mock<IEventBus> _fakeEventBus = new();

    public RegisterPassTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new PassesDatabaseConfiguration(database.ConnectionString!))
            .WithFakeEventBus(_fakeEventBus);
        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_contract_signed_event_Then_should_register_pass()
    {
        // Arrange
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemory);
        var @event = ContractSignedEventFaker.Create();

        // Act
        await integrationEventHandlerScope.Consume(@event);

        // Assert
        EnsureThatPassRegisteredEventWasPublished();
    }
    
    private void EnsureThatPassRegisteredEventWasPublished() => _fakeEventBus.Verify(
        eventBus => eventBus.PublishAsync(
            It.IsAny<PassRegisteredEvent>(), 
            It.IsAny<CancellationToken>()),
        Times.Once);
}