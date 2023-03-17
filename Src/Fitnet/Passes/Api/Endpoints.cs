namespace SuperSimpleArchitecture.Fitnet.Passes.Api;

using GetAll;
using Register;

internal static class Endpoints
{
    public static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapRegister();
        app.MapGetAll();
    }
}