namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract;

using Common.Validation.Requests;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app) => app.MapPatch(ContractsApiPaths.Sign,
            async (Guid id, SignContractRequest request,
                ContractService service,
                CancellationToken cancellationToken) =>
            {
                var contract =
                    await service.FindContractAsync(id);
                await service.SignContractAsync(contract!, request.SignedAt, cancellationToken);
                return contract is null ? Results.NotFound() : Results.NoContent();
            })
        .ValidateRequest<SignContractRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Signs prepared contract",
            Description = "This endpoint is used to sign prepared contract by customer."
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
