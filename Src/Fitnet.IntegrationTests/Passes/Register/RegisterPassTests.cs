namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Passes.Register;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes;
using Fitnet.Passes.GetAll.ViewModels;
using Fitnet.Passes.Register;

public sealed class RegisterPassTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;
    private readonly Faker _faker = new();

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory, 
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithConnectionString(ConfigurationKeys.PassesConnectionString, database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_pass_registration_request_When_processed_Then_pass_should_be_available_on_passes_list()
    {
        // Arrange
        var registerPassRequest = new RegisterPassRequest(Guid.NewGuid(), _faker.Date.Recent(),_faker.Date.Future());
        
        // Act
        var registerPassResponse = await _applicationHttpClient.PostAsJsonAsync(ApiPaths.Passes, registerPassRequest);
       
        // Assert
        registerPassResponse.EnsureSuccessStatusCode();
        var getAllPassesResponse = await GetAllPasses();
        getAllPassesResponse.Passes.Should().Contain(pass => pass.CustomerId == registerPassRequest.CustomerId);
    }
    
    private async Task<PassesResponse> GetAllPasses() => 
        await _applicationHttpClient.GetFromJsonAsync<PassesResponse>(ApiPaths.Passes);
}