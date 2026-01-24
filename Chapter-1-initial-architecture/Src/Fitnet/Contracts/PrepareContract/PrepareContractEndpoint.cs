namespace EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;

using Common.Validation.Requests;
using Data;
using Data.Database;
using Microsoft.EntityFrameworkCore;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app) =>
        app.MapPost(ContractsApiPaths.Prepare,
                async (PrepareContractRequest request,
                    ContractsPersistence persistence,
                    CancellationToken cancellationToken) =>
                {
                    var previousContract =
                        await GetPreviousForCustomerAsync(persistence, request.CustomerId, cancellationToken);
                    var contract = Contract.Prepare(
                        request.CustomerId,
                        request.CustomerAge,
                        request.CustomerHeight,
                        request.PreparedAt,
                        previousContract?.Signed);
                    await persistence.Contracts.AddAsync(contract, cancellationToken);
                    await persistence.SaveChangesAsync(cancellationToken);

                    return Results.Created($"/{ContractsApiPaths.Prepare}/{contract.Id}", contract.Id);
                })
            .ValidateRequest<PrepareContractRequest>()
            .WithSummary("Triggers preparation of a new contract for new or existing customer")
            .WithDescription("This endpoint is used to prepare a new contract for new and existing customers.")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<Contract?> GetPreviousForCustomerAsync(ContractsPersistence persistence, Guid customerId,
        CancellationToken cancellationToken = default) =>
        await persistence.Contracts
            .OrderByDescending(contract => contract.PreparedAt)
            .SingleOrDefaultAsync(contract => contract.CustomerId == customerId, cancellationToken);
}
