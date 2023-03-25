namespace SuperSimpleArchitecture.Fitnet.Passes;

using RegisterPass;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {   
        app.MapRegisterPass();
        app.MapMarkPassAsExpired();
    }
}