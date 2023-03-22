namespace SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;

internal static class PrepareContractEndpoint
{
    internal static void MapPrepareContract(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiPaths.Contracts, () =>
        {
            var fakeContractId = Guid.NewGuid();
            
            return Task.FromResult(Results.Created($"/{ApiPaths.Contracts}/{fakeContractId}", fakeContractId));
        });
    }
}