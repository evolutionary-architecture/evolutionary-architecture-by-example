namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using DataRetriever;
using EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

internal static class GenerateNewPassesPerMonthReportModule
{
    internal static IServiceCollection AddNewPassesRegistrationsPerMonthReport(this IServiceCollection services)
    {
        services.AddSingleton<INewPassesRegistrationPerMonthReportDataRetriever, NewPassesRegistrationPerMonthReportDataRetriever>();
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}