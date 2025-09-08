namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

internal static class DataAccessModule
{
    internal static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ReportsPersistenceOptions>(configuration.GetSection(ReportsPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<ReportsPersistenceOptions>();
        services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}
