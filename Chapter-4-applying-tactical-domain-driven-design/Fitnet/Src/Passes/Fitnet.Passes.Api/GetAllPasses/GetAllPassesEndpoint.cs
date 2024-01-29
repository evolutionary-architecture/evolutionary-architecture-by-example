namespace EvolutionaryArchitecture.Fitnet.Passes.Api.GetAllPasses;

using DataAccess.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

internal static class GetAllPassesEndpoint
{
    internal static void MapGetAllPasses(this IEndpointRouteBuilder app) =>
        app.MapGet(PassesApiPaths.GetAll, async (PassesPersistence persistence, CancellationToken cancellationToken) =>
            {
                var passes = await persistence.Passes
                    .AsNoTracking()
                    .Select(passes => PassDto.From(passes))
                    .ToListAsync(cancellationToken);
                var response = GetAllPassesResponse.Create(passes);

                return Results.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Returns all passes that exist in the system",
                Description =
                    "This endpoint is used to retrieve all existing passes.",
            })
            .Produces<GetAllPassesResponse>()
            .Produces(StatusCodes.Status500InternalServerError);
}