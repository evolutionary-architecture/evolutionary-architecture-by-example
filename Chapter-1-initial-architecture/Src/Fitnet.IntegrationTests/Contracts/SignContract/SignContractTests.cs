namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Contracts.SignContract;

using EvolutionaryArchitecture.Fitnet.Contracts;
using EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;
using EvolutionaryArchitecture.Fitnet.Contracts.SignContract;
using PrepareContract;
using Common.TestEngine.Configuration;
using Fitnet.Contracts.SignContract.Events;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using Microsoft.AspNetCore.Mvc;

public sealed class SignContractTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private readonly HttpClient _applicationHttpClient;
    private readonly IEventBus _fakeEventBus = Substitute.For<IEventBus>();
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;

    public SignContractTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory;
        _applicationHttpClient = applicationInMemoryFactory
            .WithFakeEventBus(_fakeEventBus)
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();
    }

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        _applicationHttpClient.Dispose();
        await _applicationInMemoryFactory.DisposeAsync();
    }

    [Fact]
    internal async Task Given_valid_contract_signature_request_Then_should_return_no_content_status_code()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        using var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest, TestContext.Current.CancellationToken);

        // Assert
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    internal async Task Given_valid_contract_signature_request_Then_contract_signed_event_was_published()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest, TestContext.Current.CancellationToken);

        // Assert
        EnsureThatContractSignedEventWasPublished();
    }

    private void EnsureThatContractSignedEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<ContractSignedEvent>(), Arg.Any<CancellationToken>());

    [Fact]
    internal async Task Given_contract_signature_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var requestParameters = SignContractRequestParameters.GetWithNotExistingContractId();
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        using var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest, TestContext.Current.CancellationToken);

        // Assert
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    internal async Task
        Given_contract_signature_request_with_invalid_signed_date_Then_should_return_conflict_status_code()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters =
            SignContractRequestParameters.GetWithInvalidSignedAtDate(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        using var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest, TestContext.Current.CancellationToken);

        // Assert
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var responseMessage = await signContractResponse.Content.ReadFromJsonAsync<ProblemDetails>(TestContext.Current.CancellationToken);
        responseMessage?.Status.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Title.ShouldBe("Contract can not be signed because more than 30 days have passed from the contract preparation");
    }

    private async Task<Guid> PrepareContract()
    {
        var requestParameters = PrepareContractRequestParameters.GetValid();
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);
        using var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);
        var preparedContractId = await prepareContractResponse.Content.ReadFromJsonAsync<Guid>();

        return preparedContractId;
    }
}
