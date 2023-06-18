namespace EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;

using Data;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Shared.SystemClock;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app)
    {
        app.MapPost(ContractsApiPaths.Prepare, async (PrepareContractRequest request, ContractsPersistence persistence, CancellationToken cancellationToken) =>
        {
            var previousContract = await GetPreviousForCustomerAsync(persistence, request.CustomerId, cancellationToken);
            var contract = Contract.PrepareStandard(request.CustomerId, request.CustomerAge, request.CustomerHeight, request.PreparedAt, previousContract?.Signed);
            await persistence.Contracts.AddAsync(contract, cancellationToken);
            await persistence.SaveChangesAsync(cancellationToken);

            return Results.Created($"/{ContractsApiPaths.Prepare}/{contract.Id}", contract.Id);
        });
    }
    
    private static async Task<Contract?> GetPreviousForCustomerAsync(ContractsPersistence persistence, Guid customerId, CancellationToken cancellationToken = default) =>
        await persistence.Contracts
            .OrderByDescending(contract => contract.PreparedAt)
            .SingleOrDefaultAsync(contract => contract.CustomerId == customerId, cancellationToken);
}