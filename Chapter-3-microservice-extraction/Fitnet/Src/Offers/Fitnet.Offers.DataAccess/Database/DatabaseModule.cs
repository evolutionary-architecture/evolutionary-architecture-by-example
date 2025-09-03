namespace EvolutionaryArchitecture.Fitnet.Offers.DataAccess.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

internal static class DatabaseModule
{
    private const string DatabaseConfigurationSection = "Database";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(options => configuration.GetSection(DatabaseConfigurationSection).Bind(options));
        services.AddDbContext<OffersPersistence>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
            options.UseNpgsql(databaseOptions.Value.ConnectionString);
        });

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}