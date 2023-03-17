namespace SuperSimpleArchitecture.Fitnet.Passes.Api.Register;

using Domain;
using Persistence;

internal static class RegisterEndpoint
{
    internal static void MapRegister(this IEndpointRouteBuilder app)
    {
        app.MapPost(Paths.Passes, async (RegisterPassRequest request, PassesPersistence persistance) =>
        {
            var pass = Pass.Register(request.CustomerId, request.From, request.To, request.PassType);
            await persistance.Passes.AddAsync(pass);
            await persistance.SaveChangesAsync();

            return Results.Created($"/{Paths.Passes}/{pass.Id}", pass.Id);
        });
    }

}