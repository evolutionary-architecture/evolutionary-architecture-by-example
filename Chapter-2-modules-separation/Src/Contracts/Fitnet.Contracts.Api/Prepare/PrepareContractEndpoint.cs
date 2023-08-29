namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

using Application;
using Common.Api.Validation.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app)
    {
        app.MapPost(ContractsApiPaths.Prepare, async (PrepareContractRequest request, IContractsModule contractsModule,
                CancellationToken cancellationToken) =>
            {
                var command = request.ToCommand();
                var contractId = await contractsModule.ExecuteCommandAsync(command, cancellationToken);

                return Results.Created($"/{ContractsApiPaths.Prepare}/{contractId}", contractId);
            })
            .ValidateRequest<PrepareContractRequestValidator>()
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
}