namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.TerminateBindingContract;

using Application;
using Common.Errors;
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
            var result = await contractsModule.ExecuteCommandAsync(command, cancellationToken);
            var response = result.Match(_ => Results.NoContent(), errors => errors.ToProblem());

            return response;
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Terminate Binding Contract",
            Description = "This endpoint is used to terminate a binding contract by invoking a termination.",
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
