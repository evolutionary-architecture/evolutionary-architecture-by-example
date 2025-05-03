namespace EvolutionaryArchitecture.Fitnet.Common.Api.Documentation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public static class ApiDocumentationExtensions
{
 public static void UseApiDocumentation(this IEndpointRouteBuilder app) =>
        app.MapGet("/", () => Results.Redirect("/swagger"))
            .Produces(StatusCodes.Status200OK)
            .WithTags("Documentation");
}
