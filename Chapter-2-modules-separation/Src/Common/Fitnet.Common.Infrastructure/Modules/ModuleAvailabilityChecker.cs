namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.Extensions.Configuration;

public static class ModuleAvailabilityChecker
{
    private const string EnabilityConfigName = "Enabled";

    public static bool IsModuleEnabled(this IConfiguration configuration, string module) =>
        configuration.GetSection(
                GetModuleConfiguration(module))
            .GetValue<bool>(EnabilityConfigName);

    private static string GetModuleConfiguration(string module) => $"Modules:{module}";
}
