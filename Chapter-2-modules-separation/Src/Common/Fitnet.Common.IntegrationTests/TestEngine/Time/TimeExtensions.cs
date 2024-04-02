namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Time;

public static class TimeExtensions
{
    public static WebApplicationFactory<T> WithTime<T>(
        this WebApplicationFactory<T> webApplicationFactory, FakeTimeProvider fakeSystemTimeProvider)
        where T : class => webApplicationFactory
        .WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddSingleton<TimeProvider>(fakeSystemTimeProvider)));
}
