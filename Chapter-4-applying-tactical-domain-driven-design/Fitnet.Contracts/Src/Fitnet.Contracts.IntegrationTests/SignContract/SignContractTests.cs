namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.SignContract;

using Api.SignContract;
using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using PrepareContract;

public sealed class SignContractTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
            .WithTestEventBus()
            .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_signature_request_Then_should_return_ok()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var requestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt, requestParameters.Signature);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_contract_signature_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var requestParameters = SignContractRequestParameters.GetWithNotExistingContractId();
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt, requestParameters.Signature);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    internal async Task
        Given_contract_signature_request_with_invalid_signed_date_Then_should_return_conflict()
    {
        // Arrange
        var preparedContractId = await _applicationHttpClient.PrepareContractAsync();
        var requestParameters =
            SignContractRequestParameters.GetWithInvalidSignedAtDate(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt, requestParameters.Signature);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var responseMessage = await signContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Detail.ShouldBe("Contract can only be signed within 30 days from preparation");
    }
}
