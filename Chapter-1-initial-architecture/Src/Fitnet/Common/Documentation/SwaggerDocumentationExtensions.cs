namespace EvolutionaryArchitecture.Fitnet.Common.Documentation;

internal static class ApiDocumentationExtensions
{
    internal static void UseApiDocumentation(this IEndpointRouteBuilder app) =>
        app.MapGet("/", () => Results.Redirect("/swagger"));
}
