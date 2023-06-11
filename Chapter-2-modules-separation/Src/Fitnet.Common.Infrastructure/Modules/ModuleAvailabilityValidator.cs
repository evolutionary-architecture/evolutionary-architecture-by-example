namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.FeatureManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class ModuleAvailabilityValidator
{
    public static bool IsModuleEnabled(this IServiceCollection services, string module)
    {
        var buildServiceProvider = services.BuildServiceProvider();
        var featureManager = buildServiceProvider.GetRequiredService<IFeatureManager>();
        
        return featureManager.IsEnabledAsync(module).Result;
    }

    public static bool IsModuleEnabled(this IApplicationBuilder applicationBuilder, string module)
    {
        var buildServiceProvider = applicationBuilder.ApplicationServices;
        var featureManager = buildServiceProvider.GetRequiredService<IFeatureManager>();
        
        return featureManager.IsEnabledAsync(module).Result;
    }
}