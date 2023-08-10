namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Sign;

using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app)
    {
        app.MapPatch(ContractsApiPaths.Sign, async (
            Guid id, 
            SignContractRequest request,
            IContractsModule contractsModule, CancellationToken cancellationToken) =>
        {
            var command = request.ToCommand(id);
            await contractsModule.ExecuteCommandAsync(command, cancellationToken);
            
            return Results.NoContent();
        });
    }
}