namespace EvolutionaryArchitecture.Fitnet.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

internal static class DatabaseModule
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OffersPersistenceOptions>(configuration.GetSection(OffersPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<OffersPersistenceOptions>();
        services.AddDbContext<OffersPersistence>((serviceProvider, options) =>
        {
            var persistenceOptions = serviceProvider.GetRequiredService<IOptions<OffersPersistenceOptions>>();
            var connectionString = persistenceOptions.Value.Offers;
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
