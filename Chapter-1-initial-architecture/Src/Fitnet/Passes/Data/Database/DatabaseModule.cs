namespace EvolutionaryArchitecture.Fitnet.Passes.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

internal static class DatabaseModule
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PassesPersistenceOptions>(configuration.GetSection(PassesPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<PassesPersistenceOptions>();
        services.AddDbContext<PassesPersistence>((serviceProvider, options) =>
        {
            var persistenceOptions = serviceProvider.GetRequiredService<IOptions<PassesPersistenceOptions>>();
            var connectionString = persistenceOptions.Value.Passes;
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
