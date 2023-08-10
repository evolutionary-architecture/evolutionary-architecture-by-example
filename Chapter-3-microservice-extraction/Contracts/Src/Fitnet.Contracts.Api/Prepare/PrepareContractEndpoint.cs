namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app)
    {
        app.MapPost(ContractsApiPaths.Prepare, async (PrepareContractRequest request, IContractsModule contractsModule, CancellationToken cancellationToken) =>
        {
            var command = request.ToCommand();
            var contractId = await contractsModule.ExecuteCommandAsync(command, cancellationToken);

            return Results.Created($"/{ContractsApiPaths.Prepare}/{contractId}", contractId);
        });
    }
}