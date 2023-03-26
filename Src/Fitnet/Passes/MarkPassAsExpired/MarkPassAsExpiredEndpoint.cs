namespace SuperSimpleArchitecture.Fitnet.Passes.RegisterPass;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app)
    {
        app.MapPatch(PassesApiPaths.MarkPassAsExpired, (Guid id) =>
        {
            throw new NotImplementedException();
        });
    }
}