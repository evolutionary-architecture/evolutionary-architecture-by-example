namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Shared.SystemClock;

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
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
                webHostBuilder.UseSetting(setting.Key, setting.Value);
        });

    internal static WebApplicationFactory<T> ConfigureTime<T>(this WebApplicationFactory<T> webApplicationFactory, DateTimeOffset fakeDateTimeOffset)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services => 
            services.AddSingleton<ISystemClock>(new FakeSystemClock(fakeDateTimeOffset))));
}