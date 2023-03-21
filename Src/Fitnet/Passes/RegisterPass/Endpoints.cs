namespace SuperSimpleArchitecture.Fitnet.Passes.RegisterPass;

internal static class Endpoints
{
    public static void MapPasses(this IEndpointRouteBuilder app) => 
        app.MapRegister();
}