namespace EvolutionaryArchitecture.Fitnet.Passes;

using GetAllPasses;
using MarkPassAsExpired;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPasses();
        app.MapMarkPassAsExpired();
    }
}