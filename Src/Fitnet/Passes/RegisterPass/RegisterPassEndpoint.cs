namespace SuperSimpleArchitecture.Fitnet.Passes.RegisterPass;

using Data;
using Data.Database;

internal static class RegisterEndpoint
{
    internal static void MapRegisterPass(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiPaths.Passes, async (RegisterPassRequest request, PassesPersistence persistance) =>
        {
            var pass = Pass.Register(request.CustomerId, request.From, request.To);
            await persistance.Passes.AddAsync(pass);
            await persistance.SaveChangesAsync();

            return Results.Created($"/{ApiPaths.Passes}/{pass.Id}", pass.Id);
        });
    }
}