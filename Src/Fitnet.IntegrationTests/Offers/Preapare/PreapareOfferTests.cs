namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Offers.Preapare;

using SuperSimpleArchitecture.Fitnet.Contracts;
using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes.MarkPassAsExpired.Events;
using Fitnet.Shared.Events;

public sealed class PreapareOfferTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;

    public PreapareOfferTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!);

        _applicationInMemory.CreateClient();
    }
   

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var handler = _applicationInMemory.Services.GetRequiredService<IIntegrationEventHandler<PassExpiredEvent>>();
        // Act
        await handler.Handle(new PassExpiredEvent(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now), CancellationToken.None);

        // Assert
    }
}