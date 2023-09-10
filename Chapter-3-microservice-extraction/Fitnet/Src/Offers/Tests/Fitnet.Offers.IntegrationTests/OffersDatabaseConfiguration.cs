namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;

internal sealed class OffersDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal OffersDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        { "ConnectionStrings:Offers", _connectionString }
    };
}
