namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.MarkPassAsExpired;

using Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;
using Api;
using Api.GetAllPasses;
using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.Configuration;
using Contracts.IntegrationEvents;
using IntegrationEvents;
using RegisterPass;

public sealed class MarkPassAsExpiredTests : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly StringContent EmptyContent = new(string.Empty);

    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;
    private readonly IEventBus _fakeEventBus = Substitute.For<IEventBus>();

    public MarkPassAsExpiredTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new PassesDatabaseConfiguration(database.ConnectionString!))
            .WithFakeEventBus(_fakeEventBus);
        _applicationHttpClient = _applicationInMemoryFactory.CreateClient();
    }

    [Fact]
    internal async Task Given_valid_mark_pass_as_expired_request_Then_should_return_no_content()
    {
        // Arrange
        var contractSigned = await RegisterPass();
        var registeredPassId = await GetCreatedPass(contractSigned.ContractCustomerId);
        var url = BuildUrl(registeredPassId);

        // Act
        await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        EnsureThatPassExpiredEventWasPublished();
    }

    [Fact]
    internal async Task Given_valid_mark_pass_as_expired_request_Then_should_publish_pass_expired_event()
    {
        // Arrange
        var contractSigned = await RegisterPass();
        var registeredPassId = await GetCreatedPass(contractSigned.ContractCustomerId);
        var url = BuildUrl(registeredPassId);

        // Act
        var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        markAsExpiredResponse.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    internal async Task Given_mark_pass_as_expired_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var notExistingId = Guid.NewGuid();
        var url = BuildUrl(notExistingId);

        // Act
        var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        markAsExpiredResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    private async Task<ContractSignedEvent> RegisterPass()
    {
        var @event = ContractSignedEventFaker.Create();
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemoryFactory);
        await integrationEventHandlerScope.Consume(@event);

        return @event;
    }

    private async Task<Guid> GetCreatedPass(Guid customerId)
    {
        var getAllPassesResponse = await _applicationHttpClient.GetAsync(PassesApiPaths.GetAll);
        var response = await getAllPassesResponse.Content.ReadFromJsonAsync<GetAllPassesResponse>();
        var createdPass = response!.Passes.FirstOrDefault(pass => pass.CustomerId == customerId);
        createdPass.Should().NotBeNull();

        return createdPass!.Id;
    }

    private static string BuildUrl(Guid id) => PassesApiPaths.MarkPassAsExpired.Replace("{id}", id.ToString());

    private void EnsureThatPassExpiredEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<PassExpiredEvent>(), Arg.Any<CancellationToken>());
}
