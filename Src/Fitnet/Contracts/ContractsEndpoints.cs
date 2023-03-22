namespace SuperSimpleArchitecture.Fitnet.Contracts;

using PrepareContract;

internal static class ContractsEndpoints
{
    public static void MapContracts(this IEndpointRouteBuilder app) => 
        app.MapPrepareContract();
}