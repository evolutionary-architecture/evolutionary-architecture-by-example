namespace SuperSimpleArchitecture.Fitnet.Passes.MarkPassAsExpired;

using Data.Database;
using Events;
using Shared.Events.EventBus;
using Shared.SystemClock;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app)
    {
        app.MapPatch(PassesApiPaths.MarkPassAsExpired,
            async (
                Guid id,
                PassesPersistence persistence,
                ISystemClock systemClock,
                IEventBus eventBus,
                CancellationToken cancellationToken) =>
            {
                var pass = await persistence.Passes.FindAsync(new object?[] {id}, cancellationToken);
                if (pass is null)
                    return Results.NotFound();

                pass.MarkAsExpired(systemClock.Now);
                await persistence.SaveChangesAsync(cancellationToken);
                await eventBus.PublishAsync(PassExpiredEvent.Create(pass.Id), cancellationToken);

                return Results.NoContent();
            });
    }
}