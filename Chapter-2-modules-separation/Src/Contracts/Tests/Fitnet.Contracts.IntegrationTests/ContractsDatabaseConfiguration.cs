namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;

internal sealed class ContractsDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal ContractsDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        {
          "Modules:Contracts:ConnectionStrings:Primary", _connectionString
        }
    };
}
