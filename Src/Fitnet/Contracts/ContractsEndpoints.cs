using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;

namespace SuperSimpleArchitecture.Fitnet.Contracts;

internal static class ContractsEndpoints
{
    public static void MapContracts(this IEndpointRouteBuilder app) => 
        app.MapPrepareContract();
}