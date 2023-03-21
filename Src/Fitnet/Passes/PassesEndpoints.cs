namespace SuperSimpleArchitecture.Fitnet.Passes;

using RegisterPass;

internal static class PassesEndpoints
{
    public static void MapPasses(this IEndpointRouteBuilder app) => 
        app.MapRegister();
}