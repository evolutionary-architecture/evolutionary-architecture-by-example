namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

using Microsoft.AspNetCore.Mvc.Testing;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(this WebApplicationFactory<T> webApplicationFactory, string connectionString)
        where T : class
    {

        var connectionStringsConfiguration = new Dictionary<string, string?>
        {
            {ConfigurationKeys.ContractsConnectionString, connectionString},
            {ConfigurationKeys.PassesConnectionString, connectionString},
            {ConfigurationKeys.ReportsConnectionString, connectionString}
        };

        return webApplicationFactory.UseSettings(connectionStringsConfiguration);
    }
    
    private static WebApplicationFactory<T> UseSettings<T>(this WebApplicationFactory<T> webApplicationFactory, Dictionary<string, string?> settings)
        where T : class
    {
        return webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
                webHostBuilder.UseSetting(setting.Key, setting.Value);
        });
    }
}