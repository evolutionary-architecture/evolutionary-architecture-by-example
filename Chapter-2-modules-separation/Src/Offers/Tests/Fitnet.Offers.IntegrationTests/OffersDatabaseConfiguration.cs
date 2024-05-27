namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;

internal sealed class OffersDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal OffersDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        {
            "Modules:Offers:ConnectionStrings:Primary", _connectionString
        }
    };
}
