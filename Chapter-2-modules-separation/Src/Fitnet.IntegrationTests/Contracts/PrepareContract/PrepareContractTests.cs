namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Common.Api.ErrorHandling;
using Fitnet.Contracts.Api;
using Fitnet.Contracts.Api.Prepare;

public sealed class PrepareContractTests : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public PrepareContractTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Created);
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
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ExceptionResponseMessage>();
        responseMessage?.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Message.Should().Be("Contract can not be prepared for a person who is not adult");
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
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ExceptionResponseMessage>();
        responseMessage?.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Message.Should().Be("Customer height must fit maximum limit for gym instruments");
    }
}