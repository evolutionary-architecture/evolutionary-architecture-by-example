namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using MarkPassAsExpired;
using RegisterPass;
using Microsoft.AspNetCore.Routing;

public static class PassesEndpoints
{
    public static void MapPasses(this IEndpointRouteBuilder app)
    {   
        app.MapRegisterPass();
        app.MapMarkPassAsExpired();
    }
}