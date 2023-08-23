namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;

using Core.SystemClock;
using Database;
using Infrastructure.Events.EventBus;
using Infrastructure.Mediator;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SystemClock;

public static class ConfigurationExtensions
{
    public static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(
        this WebApplicationFactory<T> webApplicationFactory, IDatabaseConfiguration databaseConfiguration)
        where T : class
    {
        return webApplicationFactory.UseSettings(databaseConfiguration.Get());
    }

    private static WebApplicationFactory<T> UseSettings<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        Dictionary<string, string?> settings)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
                webHostBuilder.UseSetting(setting.Key, setting.Value);
        });

    public static WebApplicationFactory<T> SetFakeSystemClock<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        DateTimeOffset fakeDateTimeOffset)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddSingleton<ISystemClock>(new FakeSystemClock(fakeDateTimeOffset))));

    public static WebApplicationFactory<T> WithFakeEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        IEventBus eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddSingleton(eventBusMock)));

    public static WebApplicationFactory<T> WithFakeConsumers<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        Assembly executingAssembly)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddMediator(executingAssembly)));
}