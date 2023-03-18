namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Passes.Register;

using Common.TestEngine;
using Common.TestEngine.Configuration;
using Fitnet.Passes.Api;
using Fitnet.Passes.Api.GetAll.ViewModels;
using Fitnet.Passes.Api.Register;

public sealed class RegisterPassTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private const string PassesPersistence = "PassesPersistence";
    private readonly HttpClient _applicationHttpClient;
    private readonly Faker _faker = new();

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory, 
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithConnectionString(PassesPersistence, database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_pass_registration_request_When_processed_Then_pass_should_be_available_on_passes_list()
    {
        // Arrange
        var registerPassRequest = new RegisterPassRequest(Guid.NewGuid(), _faker.Date.Recent(),_faker.Date.Future());
        
        // Act
        var response = await _applicationHttpClient.PostAsJsonAsync(Paths.Passes, registerPassRequest);
       
        // Assert
        response.EnsureSuccessStatusCode();
        var all = await _applicationHttpClient.GetFromJsonAsync<PassesListViewModel>(Paths.Passes);
        all.Passes.Should().Contain(listItemDto => listItemDto.CustomerId == registerPassRequest.CustomerId);
    }
}