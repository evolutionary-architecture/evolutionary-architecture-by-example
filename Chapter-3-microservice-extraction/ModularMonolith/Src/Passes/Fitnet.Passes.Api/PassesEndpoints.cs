namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using GetAllPasses;
using MarkPassAsExpired;
using Microsoft.AspNetCore.Routing;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapMarkPassAsExpired();
        app.MapGetAllPasses();
    }
}