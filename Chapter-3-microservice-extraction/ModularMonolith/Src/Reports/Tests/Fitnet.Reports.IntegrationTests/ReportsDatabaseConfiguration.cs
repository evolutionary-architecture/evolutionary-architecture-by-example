namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;

internal sealed class ReportsDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal ReportsDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        { "ConnectionStrings:Passes", _connectionString },
        { "ConnectionStrings:Reports", _connectionString }
    };
}
