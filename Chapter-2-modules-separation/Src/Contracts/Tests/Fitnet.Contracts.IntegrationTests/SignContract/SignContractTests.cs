namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.SignContract;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using Api;
using Api.Prepare;
using Api.Sign;
using Common.Infrastructure.Events.EventBus;
using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.Time;
using Microsoft.AspNetCore.Mvc;
using PrepareContract;

public sealed class SignContractTests : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private static readonly FakeTimeProvider FakeTimeProvider = new();
    private readonly HttpClient _applicationHttpClient;
    private readonly IEventBus _eventBus = Substitute.For<IEventBus>();

    public SignContractTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
            .WithFakeEventBus(_eventBus)
            .WithTime(FakeTimeProvider)
            .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_signature_request_Then_should_return_no_content_status_code()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    internal async Task Given_contract_signature_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var requestParameters = SignContractRequestParameters.GetWithNotExistingContractId();
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
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
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await signContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should()
            .Be("Contract can not be signed because more than 30 days have passed from the contract preparation");
    }

    private async Task<Guid> PrepareContract()
    {
        var requestParameters = PrepareContractRequestParameters.GetValid();
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);
        var preparedContractId = await prepareContractResponse.Content.ReadFromJsonAsync<Guid>();

        return preparedContractId;
    }
}
