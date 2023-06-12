namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests.MarkPassAsExpired;

using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using Api;
using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;
using RegisterPass;

public sealed class MarkPassAsExpiredTests : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private static readonly StringContent EmptyContent = new(string.Empty);

    private readonly HttpClient _applicationHttpClient;
    private readonly Mock<IEventBus> _fakeEventBus = new();

    public MarkPassAsExpiredTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new PassesDatabaseConfiguration(database.ConnectionString!))
            .WithFakeEventBus(_fakeEventBus)
            .CreateClient();

    [Fact]
    internal async Task Given_valid_mark_pass_as_expired_request_Then_should_return_no_content()
    {
        // Arrange
        var registeredPassId = await RegisterPass();
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
        var registeredPassId = await RegisterPass();
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

    private async Task<Guid> RegisterPass()
    {
        RegisterPassRequest registerPassRequest = new RegisterPassRequestFaker();
        var registerPassResponse =
            await _applicationHttpClient.PostAsJsonAsync(PassesApiPaths.Register, registerPassRequest);
        var registeredPassId = await registerPassResponse.Content.ReadFromJsonAsync<Guid>();

        return registeredPassId;
    }

    private static string BuildUrl(Guid id) => PassesApiPaths.MarkPassAsExpired.Replace("{id}", id.ToString());

    private void EnsureThatPassExpiredEventWasPublished() => _fakeEventBus.Verify(
        eventBus => eventBus.PublishAsync(It.IsAny<IIntegrationEvent>(), It.IsAny<CancellationToken>()), Times.Once);
}