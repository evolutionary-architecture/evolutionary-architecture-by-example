using Newtonsoft.Json;
using SuperSimpleArchitecture.Fitnet.IntegrationTests.Common;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

using SuperSimpleArchitecture.Fitnet.Contracts;
using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;
using Common.TestEngine;
using Common.TestEngine.Configuration;

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
    public async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
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
    public async Task Given_contract_preparation_request_with_invalid_age_Then_should_return_conflict_status_code()
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

        var responseMessage = await ResponseMessageDeserializer.Deserialize(prepareContractResponse);
        responseMessage.Should().Be("Contract can not be prepared for a person who is not adult");
    }
    
    [Fact]
    public async Task Given_contract_preparation_request_with_invalid_height_Then_should_return_conflict_status_code()
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
        
        var responseMessage = await ResponseMessageDeserializer.Deserialize(prepareContractResponse);
        responseMessage.Should()
            .Be("Contract can not be signed because more than 30 days have passed from the contract preparation");
    }
}