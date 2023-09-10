namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Passes";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<PassesPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
