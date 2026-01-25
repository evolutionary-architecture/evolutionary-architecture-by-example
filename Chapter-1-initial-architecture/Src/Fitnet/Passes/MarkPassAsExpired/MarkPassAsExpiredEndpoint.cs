namespace EvolutionaryArchitecture.Fitnet.Passes.MarkPassAsExpired;

using Data.Database;
using Events;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app) => app.MapPatch(
            PassesApiPaths.MarkPassAsExpired,
            async (
                Guid id,
                PassesPersistence persistence,
                TimeProvider timeProvider,
                IEventBus eventBus,
                CancellationToken cancellationToken) =>
            {
                var pass = await persistence.Passes.FindAsync([id], cancellationToken: cancellationToken);
                if (pass is null)
                {
                    return Results.NotFound();
                }

                var nowDate = timeProvider.GetUtcNow();
                pass.MarkAsExpired(nowDate);
                await persistence.SaveChangesAsync(cancellationToken);
                await eventBus.PublishAsync(
                    PassExpiredEvent.Create(pass.Id, pass.CustomerId, timeProvider.GetUtcNow()),
                    cancellationToken);

                return Results.NoContent();
            })
        .WithSummary("Marks pass which expired")
        .WithDescription("This endpoint is used to mark expired pass. Based on that it is possible to offer new contract to customer.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
}
