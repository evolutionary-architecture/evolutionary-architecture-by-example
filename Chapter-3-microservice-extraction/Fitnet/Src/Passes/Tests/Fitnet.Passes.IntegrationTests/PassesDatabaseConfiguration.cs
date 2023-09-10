namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;

internal sealed class PassesDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal PassesDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        { "ConnectionStrings:Passes", _connectionString }
    };
}
