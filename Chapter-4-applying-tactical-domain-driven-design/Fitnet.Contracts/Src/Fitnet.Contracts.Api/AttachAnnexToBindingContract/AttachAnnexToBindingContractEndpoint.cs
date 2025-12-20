namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.AttachAnnexToBindingContract;

using Common.Errors;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class AttachAnnexToBindingContractEndpoint
{
    internal static void MapAttachAnnexToBindingContract(this IEndpointRouteBuilder app) =>
        app.MapPost(
                ContractsApiPaths.AttachAnnex, async (
                        Guid id,
                        AttachAnnexToBindingContractRequest request,
                        IContractsModule contractsModule,
                        CancellationToken cancellationToken) =>
                    await contractsModule.ExecuteCommandAsync(request.ToCommand(id), cancellationToken)
                        .Match(annexId => Results.Created(BuildUrl(id, annexId), annexId),
                            errors => errors.ToProblem()))
            .WithSummary("Attach annex to existing binding contract")
            .WithDescription("This endpoint is used to attach an annex to an existing binding contract.")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

    private static string BuildUrl(Guid bindingContractId, Guid annexId)
    {
        var annexesPath = ContractsApiPaths.GetAnnexesPath(bindingContractId);

        return Path.Combine(annexesPath, annexId.ToString());
    }
}
