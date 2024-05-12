namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.TerminateBindingContract;

using Application;
using Common.Errors;
using EvolutionaryArchitecture.Fitnet.Contracts.Application.TerminateBindingContract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

internal static class TerminateBindingContractEndpoint
{
    internal static void MapTerminateBindingContract(this IEndpointRouteBuilder app) => app.MapPatch(
            ContractsApiPaths.Terminate, async (
                    [FromRoute(Name = "id")] Guid bindingContractId,
                    IContractsModule contractsModule, CancellationToken cancellationToken) =>
                await contractsModule.ExecuteCommandAsync(new TerminateBindingContractCommand(bindingContractId),
                        cancellationToken)
                    .Match(
                        _ => Results.NoContent(),
                        errors => errors.ToProblem()))
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
