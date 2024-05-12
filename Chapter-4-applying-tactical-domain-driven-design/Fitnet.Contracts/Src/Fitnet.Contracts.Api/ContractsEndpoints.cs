namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using AttachAnnexToBindingContract;
using Microsoft.AspNetCore.Routing;
using PrepareContract;
using SignContract;
using TerminateBindingContract;

internal static class ContractsEndpoints
{
    internal static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapPrepareContract();
        app.MapSignContract();
        app.MapTerminateBindingContract();
        app.MapAttachAnnexToBindingContract();
    }
}
