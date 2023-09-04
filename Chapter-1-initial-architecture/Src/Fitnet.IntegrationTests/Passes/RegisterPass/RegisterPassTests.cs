namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using Fitnet.Contracts.SignContract.Events;
using Fitnet.Passes.RegisterPass.Events;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;

public sealed class RegisterPassTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;
    private readonly IEventBus _fakeEventBus = Substitute.For<IEventBus>();

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
                .WithContainerDatabaseConfigured(database.ConnectionString!)
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

    private void EnsureThatPassRegisteredEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<PassRegisteredEvent>(), Arg.Any<CancellationToken>());
}
