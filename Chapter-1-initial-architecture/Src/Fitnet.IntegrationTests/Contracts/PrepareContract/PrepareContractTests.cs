namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

using EvolutionaryArchitecture.Fitnet.Contracts;
using EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;
using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Contracts.SignContract;
using Microsoft.AspNetCore.Mvc;
using SignContract;

public sealed class PrepareContractTests(
    WebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(database.ConnectionString!)
        .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();

        // Act
        using var prepareContractResponse = await PrepareCorrectContract(requestParameters);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_valid_contract_preparation_request_When_preparing_new_contract_for_same_customer_after_previous_contract_was_signed_Then_should_return_created_status_code()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var requestParameters = PrepareContractRequestParameters.GetValid();
        var prepareContractResponse = await PrepareCorrectContract(requestParameters, customerId);
        var preparedContractId = await prepareContractResponse.Content.ReadFromJsonAsync<Guid>(TestContext.Current.CancellationToken);
        var signContractRequestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(signContractRequestParameters.SignedAt);
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(signContractRequestParameters.Url, signContractRequest, TestContext.Current.CancellationToken);
        signContractResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        // Act
        var secondContractPreparationResponse = await PrepareCorrectContract(requestParameters, customerId);

        // Assert
        secondContractPreparationResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_age_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetWithInvalidAge();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        using var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest, TestContext.Current.CancellationToken);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>(TestContext.Current.CancellationToken);
        responseMessage?.Status.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Title.ShouldBe("Contract can not be prepared for a person who is not adult");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_height_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetWithInvalidHeight();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        using var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest, TestContext.Current.CancellationToken);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>(TestContext.Current.CancellationToken);
        responseMessage?.Status.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Title.ShouldBe("Customer height must fit maximum limit for gym instruments");
    }

    [Fact]
    internal async Task
        Given_contract_preparation_request_When_contract_for_customer_was_prepared_earlier_and_was_not_signed_yet_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();
        var customerId = Guid.NewGuid();
        await PrepareCorrectContract(requestParameters, customerId);

        //Act
        using var prepareContractResponse = await PrepareCorrectContract(requestParameters, customerId);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>(TestContext.Current.CancellationToken);
        responseMessage?.Status.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Title.ShouldBe("Previous contract must be signed by the customer");
    }

    private async Task<HttpResponseMessage> PrepareCorrectContract(PrepareContractRequestParameters requestParameters,
        Guid? customerId = null)
    {
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight, customerId);
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        return prepareContractResponse;
    }

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        _applicationHttpClient.Dispose();
        await applicationInMemoryFactory.DisposeAsync();
    }
}
