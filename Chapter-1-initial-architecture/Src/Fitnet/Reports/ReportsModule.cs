namespace SuperSimpleArchitecture.Fitnet.Reports;

using DataAccess;
using GenerateNewPassesRegistrationsPerMonthReport;
using GenerateNewPassesRegistrationsPerMonthReport.DataRetriever;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services)
    {
        services.AddDataAccess();
        services.AddNewPassesRegistrationsPerMonthReport();

        return services;
    } 

    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) => 
        applicationBuilder;
}