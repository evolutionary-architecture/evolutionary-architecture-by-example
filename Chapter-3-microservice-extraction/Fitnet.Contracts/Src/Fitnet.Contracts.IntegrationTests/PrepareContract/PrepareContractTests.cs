namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.PrepareContract;

using Common.IntegrationTestsToolbox.TestEngine;
using Common.IntegrationTestsToolbox.TestEngine.Configuration;
using Common.IntegrationTestsToolbox.TestEngine.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using Api;
using Api.Prepare;

public sealed class PrepareContractTests(FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
            .WithTestEventBus()
            .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();

        // Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_age_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetWithInvalidAge();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ExceptionResponseMessage>();
        responseMessage?.StatusCode.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Message.ShouldBe("Contract can not be prepared for a person who is not adult");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_height_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetWithInvalidHeight();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ExceptionResponseMessage>();
        responseMessage?.StatusCode.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Message.ShouldBe("Customer height must fit maximum limit for gym instruments");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_When_contract_for_customer_was_prepared_earlier_and_was_not_signed_yet_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();
        var customerId = Guid.NewGuid();
        await PrepareCorrectContract(requestParameters, customerId);

        //Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters, customerId);

        // Assert
        prepareContractResponse.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ExceptionResponseMessage>();
        responseMessage?.StatusCode.ShouldBe((int)HttpStatusCode.Conflict);
        responseMessage?.Message.ShouldBe("Previous contract must be signed by the customer");
    }

    private async Task<HttpResponseMessage> PrepareCorrectContract(PrepareContractRequestParameters requestParameters, Guid? customerId = null)
    {
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight, customerId);
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        return prepareContractResponse;
    }
}
