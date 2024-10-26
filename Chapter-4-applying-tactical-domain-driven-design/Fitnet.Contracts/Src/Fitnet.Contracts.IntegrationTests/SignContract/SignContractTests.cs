namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.SignContract;

using Api.SignContract;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using Fitnet.Common.IntegrationTestsToolbox.TestEngine;
using Fitnet.Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Fitnet.Common.IntegrationTestsToolbox.TestEngine.EventBus;
using Microsoft.AspNetCore.Mvc;
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
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.Created);
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
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
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
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await signContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Detail.Should()
            .Be("Contract can only be signed within 30 days from preparation");
    }
}
