namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationTests;

internal static class DatabaseConfiguration
{
    private const string PassesConnectionString = "ConnectionStrings:Passes";

    internal static Dictionary<string, string?> Get(string connectionString)
    {
        return new Dictionary<string, string?>
        {
            { PassesConnectionString, connectionString },
        };
    }
}