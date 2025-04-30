namespace EvolutionaryArchitecture.Fitnet.Common.Documentation;

using Microsoft.OpenApi.Models;

internal static class ApiDocumentationExtensions
{
    internal static void UseApiDocumentation(this IEndpointRouteBuilder app) =>
        app.MapGet("/", () => Results.Redirect("/swagger"))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Documentation for the API",
                Description = "This endpoint is used to redirect to the documentation for the API.",
            })
            .Produces(StatusCodes.Status200OK)
            .WithTags("Documentation");
}
