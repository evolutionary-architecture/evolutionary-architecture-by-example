namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repositories;

internal static class DatabaseModule
{
    private const string DatabaseConfigurationSection = "Database";
    private const string PostgresConnectionName = "fitnet";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(options => configuration.GetSection(DatabaseConfigurationSection).Bind(options));
        services.AddDbContext<ContractsPersistence>((serviceProvider, options) =>
        {
            // Try to get Aspire connection string first
            var connectionString = configuration.GetConnectionString(PostgresConnectionName);
            
            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback to legacy configuration
                var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
                connectionString = databaseOptions.Value.ConnectionString;
            }
            
            options.UseNpgsql(connectionString);
        });
        services.AddRepositories();

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
