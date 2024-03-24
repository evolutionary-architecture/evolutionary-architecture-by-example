namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.FeatureManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public sealed class ModuleAvailabilityChecker : IDisposable
{
    private readonly IFeatureManager _featureManager;
    private readonly ServiceProvider _provider;

    private ModuleAvailabilityChecker(IFeatureManager featureManager, ServiceProvider provider)
    {
        _featureManager = featureManager;
        _provider = provider;
    }

    public static ModuleAvailabilityChecker Build(IConfiguration configuration)
    {
        var featureFlagServiceProvider = new ServiceCollection()
            .AddSingleton(configuration)
            .AddFeatureManagement()
            .Services
            .BuildServiceProvider();
        var featureManager = featureFlagServiceProvider.GetRequiredService<IFeatureManager>();

        return new ModuleAvailabilityChecker(featureManager, featureFlagServiceProvider);
    }

    public bool IsModuleEnabled(string module) =>
        _featureManager.IsEnabledAsync(module).GetAwaiter().GetResult();

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _provider.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

public static class ModuleAvailabilityCheckerExtensions
{
    public static bool IsModuleEnabled(this IApplicationBuilder applicationBuilder, string module)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var featureManager = scope.ServiceProvider.GetRequiredService<IFeatureManager>();

        var enabled = featureManager.IsEnabledAsync(module).GetAwaiter().GetResult();

        return enabled;
    }
}
