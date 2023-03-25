namespace SuperSimpleArchitecture.Fitnet.Passes.RegisterPass;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app)
    {
        app.MapPatch($"{ApiPaths.Passes}/{{id}}/mark-as-expired", (Guid _) =>
        {
            throw new NotImplementedException();
        });
    }
}