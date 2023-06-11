namespace EvolutionaryArchitecture.Fitnet.Offers.IntegrationTests;

internal static class DatabaseConfiguration
{
    private const string OffersConnectionString = "ConnectionStrings:Offers";

    internal static Dictionary<string, string?> Get(string connectionString)
    {
        return new Dictionary<string, string?>
        {
            { OffersConnectionString, connectionString },
        };
    }
}