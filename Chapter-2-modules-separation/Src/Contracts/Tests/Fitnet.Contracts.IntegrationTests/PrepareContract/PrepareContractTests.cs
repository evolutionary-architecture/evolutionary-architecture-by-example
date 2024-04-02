namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.PrepareContract;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;
using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;
using Api;
using Api.Prepare;
using Common.IntegrationTests.TestEngine;
using Common.IntegrationTests.TestEngine.Time;
using Microsoft.AspNetCore.Mvc;

public sealed class PrepareContractTests(
    FitnetWebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<FitnetWebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private static readonly FakeTimeProvider FakeTimeProvider = new();
    private HttpClient ApplicationHttpClient => applicationInMemoryFactory
        .WithContainerDatabaseConfigured(new ContractsDatabaseConfiguration(database.ConnectionString!))
        .WithTime(FakeTimeProvider)
        .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();

        // Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters);

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
            await ApplicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should()
            .Be("Contract can not be prepared for a person who is not adult");
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
            await ApplicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should()
            .Be("Customer height must fit maximum limit for gym instruments");
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
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should()
            .Be("Previous contract must be signed by the customer");
    }

    private async Task<HttpResponseMessage> PrepareCorrectContract(PrepareContractRequestParameters requestParameters, Guid? customerId = null)
    {
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight, customerId);
        var prepareContractResponse =
            await ApplicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        return prepareContractResponse;
    }
}
