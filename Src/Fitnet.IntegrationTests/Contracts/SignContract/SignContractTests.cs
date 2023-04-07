using SuperSimpleArchitecture.Fitnet.Contracts;
using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;
using SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.SignContract;

using Common.TestEngine;
using Common.TestEngine.Configuration;

public sealed class SignContractTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public SignContractTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_contract_signature_request_Then_should_return_no_content_status_code()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var url = BuildUrl(preparedContractId);
        var signContractRequest = new SignContractRequestFaker();

        // Act
        var signContractResponse = await _applicationHttpClient.PatchAsJsonAsync(url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Given_contract_signature_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var notExistingId = Guid.NewGuid();
        var url = BuildUrl(notExistingId);
        var signContractRequest = new SignContractRequestFaker();

        // Act
        var signContractResponse = await _applicationHttpClient.PatchAsJsonAsync(url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    private async Task<Guid> PrepareContract()
    {
        const int minAge = 18;
        const int maxAge = 100;
        const int minHeight = 0;
        const int maxHeight = 210;
        
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(minAge, maxAge, minHeight, maxHeight);
        var prepareContractResponse = await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);
        var preparedContractId = await prepareContractResponse.Content.ReadFromJsonAsync<Guid>();

        return preparedContractId;
    }

    private static string BuildUrl(Guid id) => ContractsApiPaths.Prepare.Replace("{id}", id.ToString());
}