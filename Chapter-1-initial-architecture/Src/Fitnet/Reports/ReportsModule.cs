namespace EvolutionaryArchitecture.Fitnet.Reports;

using DataAccess;
using GenerateNewPassesRegistrationsPerMonthReport;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration);
        services.AddNewPassesRegistrationsPerMonthReport();

        return services;
    }

    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) =>
        applicationBuilder;
}
