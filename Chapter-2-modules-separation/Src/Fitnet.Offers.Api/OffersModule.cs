using EvolutionaryArchitecture.Fitnet.Offers.DataAccess;

namespace EvolutionaryArchitecture.Fitnet.Offers.Api;

using Common.Infrastructure.Mediator;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class OffersModule
{
    public static IServiceCollection AddOffers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration);
        services.AddMediator(Assembly.GetExecutingAssembly());

        return services;
    }
    
    public static IApplicationBuilder UseOffers(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDataAccess();

        return applicationBuilder;
    }
}