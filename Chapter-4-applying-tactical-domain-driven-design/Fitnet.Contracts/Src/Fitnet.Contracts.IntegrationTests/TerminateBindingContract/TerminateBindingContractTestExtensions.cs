namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.TerminateBindingContract;

using Api;

internal static class TerminateBindingContractTestExtensions
{
    internal static async Task TerminateBindingContractAsync(this HttpClient httpClient, Guid contractId)
    {
        var response = await httpClient.PatchAsync(GetUrl(contractId), null);
        response.EnsureSuccessStatusCode();
    }

    internal static string GetUrl(Guid bindingContractId) =>
        ContractsApiPaths.Terminate.Replace("{id}", bindingContractId.ToString());
}
