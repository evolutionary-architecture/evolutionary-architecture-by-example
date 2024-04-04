namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Time;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public static class TimeExtensions
{
    public static WebApplicationFactory<T> WithTime<T>(
        this WebApplicationFactory<T> webApplicationFactory, FakeTimeProvider fakeTimeProvider)
        where T : class => webApplicationFactory
        .WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.AddSingleton<TimeProvider>(fakeTimeProvider)));
}
