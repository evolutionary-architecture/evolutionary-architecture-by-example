namespace EvolutionaryArchitecture.Fitnet.Modules;

using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;
using Contracts.Api;
using Offers.Api;
using Passes.Api;
using Reports;

internal static class ModulesRegistry
{
    internal static void AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        using var moduleAvailabilityChecker = ModuleAvailabilityChecker.Create(configuration);
        services.AddContracts(Module.Contracts, configuration, moduleAvailabilityChecker);
        services.AddPasses(Module.Passes, configuration, moduleAvailabilityChecker);
        services.AddOffers(Module.Offers, configuration, moduleAvailabilityChecker);
        services.AddReports(Module.Reports, moduleAvailabilityChecker);
    }

    internal static void RegisterModules(this WebApplication app)
    {
        app.RegisterContracts(Module.Contracts);
        app.RegisterPasses(Module.Passes);
        app.RegisterOffers(Module.Offers);
        app.RegisterReports(Module.Reports);
    }
}
