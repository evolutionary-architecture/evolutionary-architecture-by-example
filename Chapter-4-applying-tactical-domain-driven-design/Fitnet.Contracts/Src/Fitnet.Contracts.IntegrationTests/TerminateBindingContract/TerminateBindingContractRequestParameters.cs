namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.TerminateBindingContract;

using Api;

internal record TerminateBindingContractRequestParameters(string Url)
{
    internal static TerminateBindingContractRequestParameters GetValid(Guid bindingContractId) =>
        new(BuildUrl(bindingContractId));

    private static string BuildUrl(Guid bindingContractId) =>
        ContractsApiPaths.Terminate.Replace("{id}", bindingContractId.ToString());
}
