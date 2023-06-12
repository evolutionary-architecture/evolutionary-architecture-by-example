namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests;

public class FitnetWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddJsonFile("appsettings.IntegrationTests.json", optional: true);
        });
    }
}