namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Microsoft.AspNetCore.Routing;
using Prepare;
using Sign;

public static class ContractsEndpoints
{
    public static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapPrepareContract();
        app.MapSignContract();
    }
}