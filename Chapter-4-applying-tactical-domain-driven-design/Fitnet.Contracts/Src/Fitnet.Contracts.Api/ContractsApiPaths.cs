namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Fitnet.Common.Api;

internal static class ContractsApiPaths
{
    private const string ContractsRootApi = $"{ApiPaths.Root}/contracts";
    private const string BindingContractsApi = $"{ApiPaths.Root}/binding-contracts";
    private const string AnnexesApi = $"{BindingContractsApi}/{{id}}/annexes";

    internal const string AttachAnnex = AnnexesApi;
    internal const string Terminate = $"{BindingContractsApi}/{{id}}/terminate";
    internal const string Prepare = ContractsRootApi;
    internal const string Sign = $"{ContractsRootApi}/{{id}}";
    internal const string BindingContracts = BindingContractsApi;

    internal static string GetPreparedContractPath(Guid contractId) =>
        Path.Combine(ContractsRootApi, contractId.ToString());

    internal static string GetAnnexesPath(Guid bindingContractId) =>
        AnnexesApi.Replace("{id}", bindingContractId.ToString());
}
