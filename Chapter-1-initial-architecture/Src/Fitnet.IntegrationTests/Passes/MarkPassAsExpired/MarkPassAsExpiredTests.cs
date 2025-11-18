namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Passes.MarkPassAsExpired;

using Fitnet.Passes;
using RegisterPass;
using Common.TestEngine.Configuration;
using Common.TestEngine.IntegrationEvents.Handlers;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using Fitnet.Contracts.SignContract.Events;
using Fitnet.Passes.GetAllPasses;
using Fitnet.Passes.MarkPassAsExpired.Events;

public sealed class MarkPassAsExpiredTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private static readonly StringContent EmptyContent = new(string.Empty);

    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;
    private readonly IEventBus _fakeEventBus = Substitute.For<IEventBus>();

    public MarkPassAsExpiredTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
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
        await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent, TestContext.Current.CancellationToken);

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
        using var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent, TestContext.Current.CancellationToken);

        // Assert
        markAsExpiredResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    internal async Task Given_mark_pass_as_expired_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var notExistingId = Guid.NewGuid();
        var url = BuildUrl(notExistingId);

        // Act
        using var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent, TestContext.Current.CancellationToken);

        // Assert
        markAsExpiredResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
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
        var createdPass = await CreatedPass(customerId);
        createdPass.ShouldNotBeNull();

        return createdPass.Id;
    }

    private async Task<PassDto?> CreatedPass(Guid customerId)
    {
        using var getAllPassesResponse = await _applicationHttpClient.GetAsync(PassesApiPaths.GetAll);
        var response = await getAllPassesResponse.Content.ReadFromJsonAsync<GetAllPassesResponse>();
        var createdPass = response!.Passes.FirstOrDefault(pass => pass.CustomerId == customerId);

        return createdPass;
    }

    private static string BuildUrl(Guid id) => PassesApiPaths.MarkPassAsExpired.Replace("{id}", id.ToString());

    private void EnsureThatPassExpiredEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<PassExpiredEvent>(), Arg.Any<CancellationToken>());

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        _applicationHttpClient.Dispose();
        await _applicationInMemoryFactory.DisposeAsync();
    }
}
