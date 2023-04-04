namespace SuperSimpleArchitecture.Fitnet.Reports;

using GenerateNewPassesPerMonthReport.DataRetriver;
using GenerateNewPassesPerMonthReport.ReportGenerator;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        services.AddSingleton<INewPassesPerMonthReportDataRetriever, NewPassesPerMonthReportDataRetriever>();
        services.AddSingleton<INewPassesPerMonthReportDataPdfReportGenerator, NewPassesPerMonthReportDataPdfReportGenerator>();

        return services;
    } 

    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) => 
        applicationBuilder;
}