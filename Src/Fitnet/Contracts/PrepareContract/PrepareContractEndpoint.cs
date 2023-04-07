namespace SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;

using Data;
using Data.Database;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app)
    {
        app.MapPost(ContractsApiPaths.Prepare, async (PrepareContractRequest request, ContractsPersistence persistence) =>
        {
            var contract = Contract.Prepare(request.CustomerAge, request.CustomerHeight, request.PreparedAt);
            await persistence.Contracts.AddAsync(contract);
            await persistence.SaveChangesAsync();

            return Results.Created($"/{ContractsApiPaths.Prepare}/{contract.Id}", contract.Id);
        });
    }
}