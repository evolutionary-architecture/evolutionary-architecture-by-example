using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

using Common.TestEngine;
using Common.TestEngine.Configuration;

public sealed class PrepareContractTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
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
        const int minAge = 18;
        const int maxAge = 100;
        const int minHeight = 0;
        const int maxHeight = 210;
        
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(minAge, maxAge, minHeight, maxHeight);  

        // Act
        var prepareContractResponse = await _applicationHttpClient.PostAsJsonAsync(ApiPaths.Contracts, prepareContractRequest);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }
}