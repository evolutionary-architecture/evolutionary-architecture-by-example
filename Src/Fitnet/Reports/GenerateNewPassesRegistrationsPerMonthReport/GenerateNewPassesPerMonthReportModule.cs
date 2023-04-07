namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using DataRetriever;
using SuperSimpleArchitecture.Fitnet.Reports.DataAccess;

internal static class GenerateNewPassesPerMonthReportModule
{
    internal static IServiceCollection AddNewPassesRegistrationsPerMonthReport(this IServiceCollection services)
    {
        services.AddSingleton<INewPassesRegistrationPerMonthReportDataRetriever, NewPassesRegistrationPerMonthReportDataRetriever>();
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}