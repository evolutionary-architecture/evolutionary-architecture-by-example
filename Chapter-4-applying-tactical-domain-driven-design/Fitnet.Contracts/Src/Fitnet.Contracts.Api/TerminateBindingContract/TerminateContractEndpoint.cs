namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.TerminateBindingContract;

using Application;
using EvolutionaryArchitecture.Fitnet.Contracts.Application.TerminateBindingContract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class TerminateContractEndpoint
{
    internal static void MapTerminateContract(this IEndpointRouteBuilder app) => app.MapPatch(ContractsApiPaths.Terminate, async (
            Guid id,
            IContractsModule contractsModule, CancellationToken cancellationToken) =>
        {
            var command = new TerminateBindingContractCommand(id);
            await contractsModule.ExecuteCommandAsync(command, cancellationToken);

            return Results.NoContent();
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Terminates binding contract",
            Description = "This endpoint is used to terminate an existing binding contract."
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
