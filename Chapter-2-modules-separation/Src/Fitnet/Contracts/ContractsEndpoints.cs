using EvolutionaryArchitecture.Fitnet.Contracts.SignContract;

namespace EvolutionaryArchitecture.Fitnet.Contracts;

using PrepareContract;

internal static class ContractsEndpoints
{
    internal static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapPrepareContract();
        app.MapSignContract();
    }
}