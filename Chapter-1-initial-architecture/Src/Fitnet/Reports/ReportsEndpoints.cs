namespace EvolutionaryArchitecture.Fitnet.Reports;

using GenerateNewPassesRegistrationsPerMonthReport;

internal static class ReportsEndpoints
{
    internal static void MapReports(this IEndpointRouteBuilder app) =>
        app.MapGenerateNewPassesRegistrationsPerMonthReport();
}
