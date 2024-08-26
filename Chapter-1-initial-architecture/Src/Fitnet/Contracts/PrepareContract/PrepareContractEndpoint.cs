namespace EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;

using Common.Validation.Requests;
using Data;
using FluentValidation;
using SignContract;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app) => app.MapPost(ContractsApiPaths.Prepare,
            async (PrepareContractRequest request,
                IValidator<PrepareContractRequest> validator,
                ContractService service,
                CancellationToken cancellationToken) =>
            {
                var contract = Contract.Create(request.CustomerId, request.PreparedAt);
                await service.PrepareContractAsync(request);
                return Results.Created($"/{ContractsApiPaths.Prepare}/{contract.Id}", contract.Id);
            })
        .ValidateRequest<PrepareContractRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Triggers preparation of a new contract for new or existing customer",
            Description =
                "This endpoint is used to prepare a new contract for new and existing customers.",
        })
        .Produces<string>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
