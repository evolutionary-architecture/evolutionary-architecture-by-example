namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

internal static class DatabaseModule
{
    private const string ConnectionStringConfigurationSection = "Modules:Contracts:ConnectionStrings:Primary";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection(ConnectionStringConfigurationSection).Value;
        services.AddDbContext<ContractsPersistence>(options => options.UseNpgsql(connectionString));
        services.AddRepositories();

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
