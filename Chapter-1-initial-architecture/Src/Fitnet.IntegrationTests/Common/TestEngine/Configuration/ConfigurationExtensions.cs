namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

using System.Reflection;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus.InMemory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(this WebApplicationFactory<T> webApplicationFactory, string connectionString)
        where T : class
    {
        var connectionStringsConfiguration = new Dictionary<string, string?>
        {
            {ConfigurationKeys.ContractsConnectionString, connectionString},
            {ConfigurationKeys.OffersConnectionString, connectionString},
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
            {
                webHostBuilder.UseSetting(setting.Key, setting.Value);
            }
        });

    internal static WebApplicationFactory<T> WithFakeEventBus<T>(this WebApplicationFactory<T> webApplicationFactory, IEventBus eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton(eventBusMock)));

    internal static WebApplicationFactory<T> WithFakeConsumers<T>(this WebApplicationFactory<T> webApplicationFactory)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddInMemoryEventBus(Assembly.GetExecutingAssembly())));
}
