namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.SignContract;

using Api.Sign;

internal static class SignContractTestExtensions
{
    public static async Task SignContractAsync(this HttpClient httpClient, Guid contractId)
    {
        var requestParameters = SignContractRequestParameters.GetValid(contractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);
        var response = await httpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);
        response.EnsureSuccessStatusCode();
    }
}
