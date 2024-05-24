namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.PrepareContract;

using EvolutionaryArchitecture.Fitnet.Common.Api.Validations;
using Application;
using Common.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app) =>
        app.MapPost(ContractsApiPaths.Prepare,
            async Task (PrepareContractRequest request, IContractsModule contractsModule, CancellationToken cancellationToken) =>
                await contractsModule.ExecuteCommandAsync(request.ToCommand(), cancellationToken).Match(
                contractId => Results.Created(ContractsApiPaths.GetPreparedContractPath(contractId), (object?)contractId),
                errors => errors.ToProblem()))
        .ValidateRequest<PrepareContractRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Triggers preparation of a new contract for new or existing customer",
            Description =
                "This endpoint is used to prepare a new contract for new and existing customers.",
        })
        .Produces<string>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
