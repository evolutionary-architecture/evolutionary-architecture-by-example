namespace EvolutionaryArchitecture.Fitnet.Common.Documentation;

internal static class ApiDocumentationExtensions
{
    internal static void UseApiDocumentation(this IEndpointRouteBuilder app) =>
        app.MapGet("/", () => Results.Redirect("/swagger"))
            .WithSummary("Documentation for the API")
            .WithDescription("This endpoint is used to redirect to the documentation for the API.")
            .Produces(StatusCodes.Status200OK)
            .WithTags("Documentation");
}
