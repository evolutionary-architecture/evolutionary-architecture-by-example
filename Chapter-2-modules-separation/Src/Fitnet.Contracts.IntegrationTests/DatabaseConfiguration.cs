namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests;

internal static class DatabaseConfiguration
{
    private const string ContractsConnectionString = "ConnectionStrings:Contracts";

    internal static Dictionary<string, string?> Get(string connectionString)
    {
        return new Dictionary<string, string?>
        {
            { ContractsConnectionString, connectionString },
        };
    }
}