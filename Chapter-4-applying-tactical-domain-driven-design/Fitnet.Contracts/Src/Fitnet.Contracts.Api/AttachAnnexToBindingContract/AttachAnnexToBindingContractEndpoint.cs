namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.TerminateBindingContract;

using Application;
using AttachAnnexToBindingContract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class AttachAnnexToBindingContractEndpoint
{
    internal static void MapAttachAnnexToBindingContract(this IEndpointRouteBuilder app) => app.MapPost(
            ContractsApiPaths.AttachAnnex, async (
                Guid id,
                AttachAnnexToBindingContractRequest request,
                IContractsModule contractsModule,
                CancellationToken cancellationToken) =>
            {
                var command = request.ToCommand(id);
                var annexId = await contractsModule.ExecuteCommandAsync(command, cancellationToken);

                return Results.Created($"/{BuildUrl(id, annexId)}", annexId);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Attach annex to existing binding contract",
            Description = "This endpoint is used to attach an annex to an existing binding contract.",
        })
        .Produces<string>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);

    private static string BuildUrl(Guid bindingContractId, Guid annexId)
    {
        var annexesPath = ContractsApiPaths.GetAnnexesPath(bindingContractId.ToString());

        return Path.Combine(annexesPath, annexId.ToString());
    }
}
