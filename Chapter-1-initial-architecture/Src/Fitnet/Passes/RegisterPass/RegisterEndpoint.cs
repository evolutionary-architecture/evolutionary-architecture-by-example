namespace EvolutionaryArchitecture.Fitnet.Passes.RegisterPass;

using Data;
using Data.Database;
using Shared.Events.EventBus;

internal static class RegisterEndpoint
{
    internal static void MapRegisterPass(this IEndpointRouteBuilder app)
    {
        app.MapPost(PassesApiPaths.Register, async (RegisterPassRequest request, PassesPersistence persistence, CancellationToken cancellationToken) =>
        {
            var pass = Pass.Register(request.CustomerId, request.From, request.To);
            await persistence.Passes.AddAsync(pass, cancellationToken);
            await persistence.SaveChangesAsync(cancellationToken);

            return Results.Created($"/{PassesApiPaths.Register}/{pass.Id}", pass.Id);
        });
    }
}