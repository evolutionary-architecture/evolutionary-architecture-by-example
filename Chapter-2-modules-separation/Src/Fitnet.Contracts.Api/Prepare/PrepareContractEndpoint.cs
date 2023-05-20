namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

using EvolutionaryArchitecture.Fitnet.Contracts.Core;
using EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app)
    {
        app.MapPost(ContractsApiPaths.Prepare, async (PrepareContractRequest request, ContractsPersistence persistence, CancellationToken cancellationToken) =>
        {
            var contract = Contract.Prepare(request.CustomerAge, request.CustomerHeight, request.PreparedAt);
            await persistence.Contracts.AddAsync(contract, cancellationToken);
            await persistence.SaveChangesAsync(cancellationToken);

            return Results.Created($"/{ContractsApiPaths.Prepare}/{contract.Id}", contract.Id);
        });
    }
}