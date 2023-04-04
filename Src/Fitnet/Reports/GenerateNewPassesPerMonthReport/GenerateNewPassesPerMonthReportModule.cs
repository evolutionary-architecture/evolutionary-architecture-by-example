namespace SuperSimpleArchitecture.Fitnet.Contracts.Data.Database;

using Reports.GenerateNewPassesPerMonthReport.DataRetriver;
using Reports.GenerateNewPassesPerMonthReport.ReportGenerator;

internal static class GenerateNewPassesPerMonthReportModule
{
    internal static IServiceCollection AddGenerateNewPassesPerMonthReport(this IServiceCollection services)
    {
        services.AddSingleton<INewPassesPerMonthReportDataPdfReportGenerator, NewPassesPerMonthReportDataPdfReportGenerator>();
        services.AddSingleton<INewPassesPerMonthReportDataRetriever, NewPassesPerMonthReportDataRetriever>();
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}