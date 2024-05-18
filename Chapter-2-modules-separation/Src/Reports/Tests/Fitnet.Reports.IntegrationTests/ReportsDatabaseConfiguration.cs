namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests;

using EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;

internal sealed class ReportsDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly string _connectionString;

    internal ReportsDatabaseConfiguration(string connectionString) => _connectionString = connectionString;

    public Dictionary<string, string?> Get() => new()
    {
        { "Modules:Passes:ConnectionStrings:Primary", _connectionString },
        { "Modules:Reports:ConnectionStrings:Primary", _connectionString }
    };
}
