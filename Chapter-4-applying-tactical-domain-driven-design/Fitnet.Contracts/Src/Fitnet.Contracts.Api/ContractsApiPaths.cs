namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Common.Api;

internal static class ContractsApiPaths
{
    private const string ContractsRootApi = $"{ApiPaths.Root}/contracts";
    private const string BindingContractsApi = $"{ApiPaths.Root}/bindingContracts";

    internal const string Terminate = $"{BindingContractsApi}/{{id}}/terminate";
    internal const string Prepare = ContractsRootApi;
    internal const string Sign = $"{ContractsRootApi}/{{id}}";
}
