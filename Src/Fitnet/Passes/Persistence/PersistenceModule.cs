namespace SuperSimpleArchitecture.Fitnet.Passes.Persistence;

using Microsoft.EntityFrameworkCore;

internal static class PersistenceModule
{
    private const string ConnectionStringName = "Passes";
    
    internal static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<PassesPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }
    
    internal static IApplicationBuilder UsePersistenceModule(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}