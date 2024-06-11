namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using DataAccess;
using System.Reflection;
using Common.EventBus;
using Fitnet.Common.Infrastructure.Mediator;
using Fitnet.Common.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PassesModule
{
    public static void RegisterPasses(this WebApplication app, string module)
    {
        if (!app.Configuration.IsModuleEnabled(module))
        {
            return;
        }

        app.UsePasses();
        app.MapPasses();
    }

    public static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration,
        string module)
    {
        if (!configuration.IsModuleEnabled(module))
        {
            return services;
        }

        services.AddDataAccess(configuration);
        services.AddMediator(Assembly.GetExecutingAssembly());
        services.AddEventBus(configuration);

        return services;
    }

    private static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDataAccess();

        return applicationBuilder;
    }
}
