namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.TerminateBindingContract;

using Common.Api.Assertions;

internal static class TerminateBindingContractTestExtensions
{
    internal static async Task TerminateBindingContractAsync(this HttpClient httpClient, Guid bindingContractId)
    {
        var request = TerminateBindingContractRequestParameters.GetValid(bindingContractId);
        var response = await httpClient.PatchAsync(request.Url, null);
        await response.EnsureSuccessStatusCodeAsync();
    }
}
