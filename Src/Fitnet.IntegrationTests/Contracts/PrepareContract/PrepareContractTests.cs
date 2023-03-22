namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

public sealed class PrepareContractTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _applicationHttpClient;

    public PrepareContractTests(WebApplicationFactory<Program> applicationInMemoryFactory) =>
        _applicationHttpClient = applicationInMemoryFactory
            .CreateClient();

    [Fact]
    public async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange

        // Act
        var prepareContractResponse = await _applicationHttpClient.PostAsync(ApiPaths.Contracts, new StringContent(string.Empty));

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }
}