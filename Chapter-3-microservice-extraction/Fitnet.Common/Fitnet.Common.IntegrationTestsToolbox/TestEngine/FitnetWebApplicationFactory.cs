namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine;

public class FitnetWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            var settingsPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "appsettings.IntegrationTestsToolbox.json");

            configBuilder.AddJsonFile(settingsPath);
        });
    }
}