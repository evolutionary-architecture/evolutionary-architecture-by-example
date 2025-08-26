namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repositories;

internal static class DatabaseModule
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // First point - register options using native SDK method with validation
        services.Configure<ContractsPersistenceOptions>(configuration.GetSection(ContractsPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<ContractsPersistenceOptions>();

        services.AddDbContext<ContractsPersistence>((serviceProvider, options) =>
        {
            var persistenceOptions = serviceProvider.GetRequiredService<IOptions<ContractsPersistenceOptions>>();
            var connectionString = persistenceOptions.Value.Primary;
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
