namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using Common.Infrastructure.Modules;
using DataAccess;
using System.Reflection;
using Common.Infrastructure.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PassesModule
{
    public static void RegisterPasses(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module)) return;

        app.UsePasses();
        app.MapPasses();
    }

    public static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration,
        string module)
    {
        if (!services.IsModuleEnabled(module)) return services;

        services.AddDataAccess(configuration);
        services.AddMediator(Assembly.GetExecutingAssembly());
        services.AddConsumers();

        return services;
    }

    private static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDataAccess();
        
        return applicationBuilder;
    }
}