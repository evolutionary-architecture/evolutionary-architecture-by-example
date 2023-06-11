namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Microsoft.AspNetCore.Routing;
using Prepare;
using Sign;

internal static class ContractsEndpoints
{
    internal static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapPrepareContract();
        app.MapSignContract();
    }
}