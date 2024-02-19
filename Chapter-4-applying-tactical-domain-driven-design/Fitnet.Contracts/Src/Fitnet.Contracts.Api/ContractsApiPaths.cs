namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Common.Api;

internal static class ContractsApiPaths
{
    private const string ContractsRootApi = $"{ApiPaths.Root}/contracts";

    internal const string Terminate = $"{ContractsRootApi}/{{id}}";
    internal const string Prepare = ContractsRootApi;
    internal const string Sign = $"{ContractsRootApi}/{{id}}";
}
