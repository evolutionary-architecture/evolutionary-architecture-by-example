namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using MarkPassAsExpired;
using RegisterPass;
using Microsoft.AspNetCore.Routing;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {   
        app.MapRegisterPass();
        app.MapMarkPassAsExpired();
    }
}