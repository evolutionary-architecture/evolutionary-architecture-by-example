namespace EvolutionaryArchitecture.Fitnet.Modules;

using Contracts.Api;
using Offers.Api;
using Passes.Api;
using Reports;

internal static class ModulesRegistry
{
    internal static void AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddContracts(Module.Contracts, configuration);
        services.AddPasses(Module.Passes, configuration);
        services.AddOffers(Module.Offers, configuration);
        services.AddReports(Module.Reports, configuration);
    }

    internal static void RegisterModules(this WebApplication app)
    {
        app.RegisterContracts(Module.Contracts);
        app.RegisterPasses(Module.Passes);
        app.RegisterOffers(Module.Offers);
        app.RegisterReports(Module.Reports);
    }
}
