namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests;

internal static class DatabaseConfiguration
{
    private const string PassesConnectionString = "ConnectionStrings:Passes";
    private const string ReportsConnectionString = "ConnectionStrings:Reports";

    internal static Dictionary<string, string?> Get(string connectionString)
    {
        return new Dictionary<string, string?>
        {
            { PassesConnectionString, connectionString },
            { ReportsConnectionString, connectionString },
        };
    }
}