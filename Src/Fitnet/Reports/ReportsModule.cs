namespace SuperSimpleArchitecture.Fitnet.Reports;

using GenerateNewPassesPerMonthReport.DataRetriver;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        services.AddSingleton<INewPassesPerMonthReportDataRetriever, NewPassesPerMonthReportDataRetriever>();

        return services;
    } 

    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) => 
        applicationBuilder;
}