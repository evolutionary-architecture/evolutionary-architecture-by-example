namespace EvolutionaryArchitecture.Fitnet.Offers.Api;

using Common.Infrastructure.Modules;
using DataAccess;
using Common.Infrastructure.Mediator;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class OffersModule
{
    public static void RegisterOffers(this WebApplication app, string module)
    {
        if (!app.Configuration.IsModuleEnabled(module))
        {
            return;
        }

        app.UseOffers();
    }

    public static IServiceCollection AddOffers(this IServiceCollection services,
        string module,
        IConfiguration configuration)
    {
        if (!configuration.IsModuleEnabled(module))
        {
            return services;
        }

        services.AddDataAccess(configuration);
        services.AddMediator(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IApplicationBuilder UseOffers(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDataAccess();

        return applicationBuilder;
    }
}
