namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

internal static class ConfigurationKeys
{
    private const string ConnectionStringsSection = "ConnectionStrings";
    internal const string PassesConnectionString = $"{ConnectionStringsSection}:Passes";
    internal const string OffersConnectionString = $"{ConnectionStringsSection}:Offers";
    internal const string ContractsConnectionString = $"{ConnectionStringsSection}:Contracts";
    internal const string ReportsConnectionString = $"{ConnectionStringsSection}:Reports";
}