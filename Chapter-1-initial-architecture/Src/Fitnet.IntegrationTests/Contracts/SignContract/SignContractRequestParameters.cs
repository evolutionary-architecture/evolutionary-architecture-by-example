namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Contracts.SignContract;

using EvolutionaryArchitecture.Fitnet.Contracts;

internal record SignContractRequestParameters(string Url, DateTimeOffset SignedAt)
{
    internal static SignContractRequestParameters GetValid(Guid id) =>
        new(BuildUrl(id), GetValidSignedAtDate());

    internal static SignContractRequestParameters GetWithNotExistingContractId() =>
        new(BuildUrl(Guid.NewGuid()), GetValidSignedAtDate());

    internal static SignContractRequestParameters GetWithInvalidSignedAtDate(Guid id) =>
        new(BuildUrl(id), DateTimeOffset.Now.AddDays(31).ToUniversalTime());

    private static string BuildUrl(Guid id) => ContractsApiPaths.Sign.Replace("{id}", id.ToString());
    private static DateTimeOffset GetValidSignedAtDate() => DateTimeOffset.Now.AddDays(1).ToUniversalTime();
}

