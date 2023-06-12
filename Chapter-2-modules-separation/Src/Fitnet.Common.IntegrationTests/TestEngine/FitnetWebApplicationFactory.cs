namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.IntegrationTests;

public class FitnetWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    public new FitnetWebApplicationFactory<T> WithWebHostBuilder(Action<IWebHostBuilder> builder)
    {
        base.WithWebHostBuilder(builder);
        return this;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddJsonFile("appsettings.IntegrationTests.json", optional: true);
        });
    }
}