namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;

using Microsoft.Extensions.Configuration;

public static class ModuleAvailabilityChecker
{
    private const string AvailabilityConfigKeyName = "Enabled";

    public static bool IsModuleEnabled(this IConfiguration configuration, string module) =>
        configuration.GetSection(
                GetModuleConfiguration(module))
            .GetValue<bool>(AvailabilityConfigKeyName);

    private static string GetModuleConfiguration(string module) => $"Modules:{module}";
}
