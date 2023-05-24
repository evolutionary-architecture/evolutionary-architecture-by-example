namespace EvolutionaryArchitecture.Fitnet.Offers;

using System.Reflection;
using Common.Infrastructure.Mediator;
using Data.Database;

internal static class OffersModule
{
    internal static IServiceCollection AddOffers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddMediator(Assembly.GetExecutingAssembly());

        return services;
    }
    
    internal static IApplicationBuilder UseOffers(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}