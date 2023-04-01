namespace SuperSimpleArchitecture.Fitnet.Reports;

using GenerateNewPassesPerMonthReport;

internal static class ReportsEndpoints
{
    internal static void MapReports(this IEndpointRouteBuilder app) => 
        app.MapGenerateNewPassesPerMonthReport();
}