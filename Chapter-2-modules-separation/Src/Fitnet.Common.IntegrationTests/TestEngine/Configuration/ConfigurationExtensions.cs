namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Configuration;

using System.Reflection;
using Infrastructure.Events.EventBus;
using Infrastructure.Mediator;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.SystemClock;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SystemClock;

public static class ConfigurationExtensions
{
    public static FitnetWebApplicationFactory<T> WithContainerDatabaseConfigured<T>(
        this FitnetWebApplicationFactory<T> webApplicationFactory, IDatabaseConfiguration databaseConfiguration)
        where T : class
    {
        return webApplicationFactory.UseSettings(databaseConfiguration.Get());
    }

    private static FitnetWebApplicationFactory<T> UseSettings<T>(
        this FitnetWebApplicationFactory<T> webApplicationFactory,
        Dictionary<string, string?> settings)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
                webHostBuilder.UseSetting(setting.Key, setting.Value);
        });

    public static FitnetWebApplicationFactory<T> SetFakeSystemClock<T>(
        this FitnetWebApplicationFactory<T> webApplicationFactory,
        DateTimeOffset fakeDateTimeOffset)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddSingleton<ISystemClock>(new FakeSystemClock(fakeDateTimeOffset))));

    public static FitnetWebApplicationFactory<T> WithFakeEventBus<T>(
        this FitnetWebApplicationFactory<T> webApplicationFactory,
        IMock<IEventBus> eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddSingleton(eventBusMock.Object)));

    public static FitnetWebApplicationFactory<T> WithFakeConsumers<T>(
        this FitnetWebApplicationFactory<T> webApplicationFactory,
        Assembly executingAssembly)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddMediator(executingAssembly)));
}