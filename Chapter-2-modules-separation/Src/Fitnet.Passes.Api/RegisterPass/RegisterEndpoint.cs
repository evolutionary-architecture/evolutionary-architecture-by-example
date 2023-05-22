namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using DataAccess;
using DataAccess.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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