namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests;

public class FitnetWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.Sources.Clear();

            var settingsPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "appsettings.IntegrationTests.json");

            configBuilder.AddJsonFile(settingsPath);
        });
    }
}