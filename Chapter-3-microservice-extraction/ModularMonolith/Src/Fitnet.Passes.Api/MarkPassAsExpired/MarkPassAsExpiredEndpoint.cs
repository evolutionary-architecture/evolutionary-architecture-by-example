namespace EvolutionaryArchitecture.Fitnet.Passes.Api.MarkPassAsExpired;

using Common.Core.SystemClock;
using Common.Infrastructure.Events.EventBus;
using IntegrationEvents;
using DataAccess.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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
               
                var passExpiredEvent = PassExpiredEvent.Create(pass.Id, pass.CustomerId);
                await eventBus.PublishAsync(passExpiredEvent, cancellationToken);

                return Results.NoContent();
            });
    }
}