namespace EvolutionaryArchitecture.Fitnet.Passes.Api.MarkPassAsExpired;

using IntegrationEvents;
using DataAccess.Database;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app) => app.MapPatch(
            PassesApiPaths.MarkPassAsExpired,
            async (
                Guid id,
                PassesPersistence persistence,
                TimeProvider timeProvider,
                IPublishEndpoint publishEndpoint,
                CancellationToken cancellationToken) =>
            {
                var pass = await persistence.Passes.FindAsync([id], cancellationToken);
                if (pass is null)
                {
                    return Results.NotFound();
                }

                pass.MarkAsExpired(timeProvider.GetUtcNow());
                await persistence.SaveChangesAsync(cancellationToken);

                var passExpiredEvent = PassExpiredEvent.Create(pass.Id, pass.CustomerId, timeProvider.GetUtcNow());
                await publishEndpoint.Publish(passExpiredEvent, cancellationToken);

                return Results.NoContent();
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Marks pass which expired",
            Description =
                "This endpoint is used to mark expired pass. Based on that it is possible to offer new contract to customer.",
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
}
