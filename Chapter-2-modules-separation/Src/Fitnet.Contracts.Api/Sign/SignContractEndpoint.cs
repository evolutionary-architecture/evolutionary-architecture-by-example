namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Sign;

using EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app)
    {
        app.MapPatch(ContractsApiPaths.Sign, async (Guid id, SignContractRequest request,
            ContractsPersistence persistence, CancellationToken cancellationToken) =>
        {
            var contract =
                await persistence.Contracts.FindAsync(new object?[] { id }, cancellationToken);
            if (contract is null)
                return Results.NotFound();

            contract.Sign(request.SignedAt);
            await persistence.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        });
    }
}