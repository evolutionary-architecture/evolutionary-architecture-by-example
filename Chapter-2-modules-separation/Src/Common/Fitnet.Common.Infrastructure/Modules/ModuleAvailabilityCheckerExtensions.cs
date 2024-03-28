namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

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
