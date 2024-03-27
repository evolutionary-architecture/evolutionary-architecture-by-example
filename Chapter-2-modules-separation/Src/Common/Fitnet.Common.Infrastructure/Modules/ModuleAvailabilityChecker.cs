namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public sealed class ModuleAvailabilityChecker : IDisposable
{
    private readonly IFeatureManager _featureManager;
    private readonly ServiceProvider _serviceProvider;

    private ModuleAvailabilityChecker(IFeatureManager featureManager, ServiceProvider serviceProvider)
    {
        _featureManager = featureManager;
        _serviceProvider = serviceProvider;
    }

    public static ModuleAvailabilityChecker Create(IConfiguration configuration)
    {
        var featureFlagServiceProvider = BuildIsolatedFeatureFlagServiceProvider(configuration);
        var featureManager = featureFlagServiceProvider.GetRequiredService<IFeatureManager>();

        return new ModuleAvailabilityChecker(featureManager, featureFlagServiceProvider);
    }

    private static ServiceProvider BuildIsolatedFeatureFlagServiceProvider(IConfiguration configuration) => new ServiceCollection()
            .AddSingleton(configuration)
            .AddFeatureManagement()
            .Services
            .BuildServiceProvider();

    public bool IsModuleEnabled(string module) =>
        _featureManager.IsEnabledAsync(module).GetAwaiter().GetResult();

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _serviceProvider.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
