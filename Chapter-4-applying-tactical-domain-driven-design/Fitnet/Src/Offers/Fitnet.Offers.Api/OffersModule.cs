namespace EvolutionaryArchitecture.Fitnet.Offers.Api;

using DataAccess;
using Common.Infrastructure.Mediator;
using System.Reflection;
using Common.Infrastructure.Modules;
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

    public static IServiceCollection AddOffers(this IServiceCollection services, IConfiguration configuration,
        string module)
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
