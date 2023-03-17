namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

using Microsoft.AspNetCore.Mvc.Testing;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithConnectionString<T>(this WebApplicationFactory<T> webApplicationFactory, string name, string value)
        where T : class =>
        webApplicationFactory.WithConfiguration($"ConnectionStrings:{name}", value);
    
    private static WebApplicationFactory<T> WithConfiguration<T>(this WebApplicationFactory<T> webApplicationFactory, string key, string value)
        where T : class
    {
        return webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string?>(key, value),
                })
                .Build();

            webHostBuilder.UseConfiguration(configuration);
        });
    }
}