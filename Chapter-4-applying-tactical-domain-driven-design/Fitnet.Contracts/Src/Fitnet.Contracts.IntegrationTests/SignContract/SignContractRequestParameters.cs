namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.SignContract;

using Api;

internal record SignContractRequestParameters(string Url, DateTimeOffset SignedAt, string Signature)
{
    internal static SignContractRequestParameters GetValid(Guid id, string signature = "John Doe") =>
        new(BuildUrl(id), GetValidSignedAtDate(), signature);

    internal static SignContractRequestParameters GetWithNotExistingContractId() =>
        new(BuildUrl(Guid.NewGuid()), GetValidSignedAtDate(), "John Doe");

    internal static SignContractRequestParameters GetWithInvalidSignedAtDate(Guid id) =>
        new(BuildUrl(id), DateTimeOffset.Now.AddDays(31).ToUniversalTime(), "John Doe");

    private static string BuildUrl(Guid id) => ContractsApiPaths.Sign.Replace("{id}", id.ToString());
    private static DateTimeOffset GetValidSignedAtDate() => DateTimeOffset.Now.AddDays(1).ToUniversalTime();
}
