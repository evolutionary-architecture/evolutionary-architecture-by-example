namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.FeatureManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable S3881
public class ModuleAvailabilityChecker : IDisposable
#pragma warning restore S3881
{
    private readonly IFeatureManager _featureManager;
    private readonly ServiceProvider _provider;

    private ModuleAvailabilityChecker(IFeatureManager featureManager, ServiceProvider provider)
    {
        _featureManager = featureManager;
        _provider = provider;
    }

    public static ModuleAvailabilityChecker Create(IConfiguration configuration)
    {
        var provider = new ServiceCollection()
            .AddSingleton(configuration)
            .AddFeatureManagement()
            .Services
            .BuildServiceProvider();
        var requiredService = provider.GetRequiredService<IFeatureManager>();

        return new ModuleAvailabilityChecker(requiredService, provider);
    }

    public bool IsModuleEnabled(string module) => _featureManager.IsEnabledAsync(module).GetAwaiter().GetResult();

    public void Dispose()
    {
        _provider.Dispose();
        GC.SuppressFinalize(this);
    }
}

public static class Test
{
    public static bool IsModuleEnabled(this IApplicationBuilder applicationBuilder, string module)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var featureManager = scope.ServiceProvider.GetRequiredService<IFeatureManager>();

        var enabled = featureManager.IsEnabledAsync(module).GetAwaiter().GetResult();

        return enabled;
    }
}
