namespace EvolutionaryArchitecture.Fitnet.Passes.GetAllPasses;

using EvolutionaryArchitecture.Fitnet.Passes;
using EvolutionaryArchitecture.Fitnet.Passes.Data.Database;
using Microsoft.EntityFrameworkCore;

internal static class GetAllEndpoint
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
        });
}