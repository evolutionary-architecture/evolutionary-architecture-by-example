namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.SignContract;

using Api.SignContract;

internal static class SignContractTestExtensions
{
    internal static async Task<Guid> SignContractAsync(this HttpClient httpClient, Guid contractId)
    {
        var requestParameters = SignContractRequestParameters.GetValid(contractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);
        var response = await httpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);
        response.EnsureSuccessStatusCode();

        var bindingContractId = await response.Content.ReadFromJsonAsync<Guid>();

        return bindingContractId;
    }
}
