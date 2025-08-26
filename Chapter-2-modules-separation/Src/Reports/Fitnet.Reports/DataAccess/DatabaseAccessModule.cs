namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DatabaseAccessModule
{
    internal static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // First point - register options using native SDK method with validation
        services.Configure<ReportsPersistenceOptions>(configuration.GetSection(ReportsPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<ReportsPersistenceOptions>();
        
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}
