namespace SuperSimpleArchitecture.Fitnet.Contracts.Data.Database;

using Reports.GenerateNewPassesPerMonthReport.DataRetriver;

internal static class GenerateNewPassesPerMonthReportModule
{
    internal static IServiceCollection AddGenerateNewPassesPerMonthReport(this IServiceCollection services)
    {
        services.AddSingleton<INewPassesPerMonthReportDataRetriever, NewPassesPerMonthReportDataRetriever>();
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}