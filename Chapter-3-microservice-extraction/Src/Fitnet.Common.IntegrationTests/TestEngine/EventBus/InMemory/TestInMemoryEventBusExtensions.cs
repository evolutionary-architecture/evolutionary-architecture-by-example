namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.EventBus.InMemory;

using Infrastructure.Events.EventBus.InMemory;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public static class TestInMemoryEventBusExtensions
{
    public static WebApplicationFactory<T> WithTestInMemoryEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        IMock<IInMemoryEventBus> eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
                services.AddSingleton(eventBusMock.Object)));
}