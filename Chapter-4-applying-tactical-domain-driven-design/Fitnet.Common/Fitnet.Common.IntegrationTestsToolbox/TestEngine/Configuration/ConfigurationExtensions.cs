namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Configuration;

using Database;
using Infrastructure.Mediator;
using MassTransit;
using Microsoft.AspNetCore.TestHost;

public static class ConfigurationExtensions
{
    public static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(
        this WebApplicationFactory<T> webApplicationFactory, IDatabaseConfiguration databaseConfiguration)
        where T : class => webApplicationFactory.UseSettings(databaseConfiguration.Get());

    private static WebApplicationFactory<T> UseSettings<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        Dictionary<string, string?> settings)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
            {
                webHostBuilder.UseSetting(setting.Key, setting.Value);
            }
        });

    public static WebApplicationFactory<T> WithFakeConsumers<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        Assembly executingAssembly)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddMediator(executingAssembly)));

    public static WebApplicationFactory<T> WithMassTransitTestHarness<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        params Type[] types)
        where T : class => webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
    {
        webHostBuilder.ConfigureServices(services => services.AddMassTransitTestHarness(cfg =>
        {
            foreach (var type in types)
            {
                cfg.AddConsumer(type);
            }
        }));
    });
}
