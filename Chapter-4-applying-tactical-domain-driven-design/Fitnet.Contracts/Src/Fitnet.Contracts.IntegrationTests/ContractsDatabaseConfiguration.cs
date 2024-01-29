namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;

internal sealed class ContractsDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal ContractsDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        { "ConnectionStrings:Contracts", _connectionString }
    };
}
