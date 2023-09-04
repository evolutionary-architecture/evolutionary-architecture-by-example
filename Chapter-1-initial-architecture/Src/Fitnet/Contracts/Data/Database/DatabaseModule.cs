namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database;

using Microsoft.EntityFrameworkCore;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Contracts";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<ContractsPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
