using SuperSimpleArchitecture.Fitnet.Contracts.SignContract;

namespace SuperSimpleArchitecture.Fitnet.Contracts;

using PrepareContract;

internal static class ContractsEndpoints
{
    internal static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapPrepareContract();
        app.MapSignContract();
    }
        
}