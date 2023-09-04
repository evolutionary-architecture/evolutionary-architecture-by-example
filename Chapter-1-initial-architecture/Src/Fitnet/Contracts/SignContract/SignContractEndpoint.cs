namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract;

using Data.Database;
using Events;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using Common.SystemClock;
using Common.Validation.Requests;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app) => app.MapPatch(ContractsApiPaths.Sign,
            async (Guid id, SignContractRequest request,
                ContractsPersistence persistence, IEventBus bus, ISystemClock systemClock,
                CancellationToken cancellationToken) =>
            {
                var contract =
                    await persistence.Contracts.FindAsync(new object?[] { id }, cancellationToken);

                if (contract is null)
                {
                    return Results.NotFound();
                }

                contract.Sign(request.SignedAt, systemClock.Now);
                await persistence.SaveChangesAsync(cancellationToken);

                var @event = ContractSignedEvent.Create(contract.Id, contract.CustomerId, contract.SignedAt!.Value,
                    contract.ExpiringAt!.Value);
                await bus.PublishAsync(@event, cancellationToken);

                return Results.NoContent();
            })
        .ValidateRequest<SignContractRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Signs prepared contract",
            Description =
                "This endpoint is used to sign prepared contract by customer.",
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
