namespace EvolutionaryArchitecture.Fitnet.Reports;

using Microsoft.AspNetCore.Routing;
using GenerateNewPassesRegistrationsPerMonthReport;

internal static class ReportsEndpoints
{
    internal static void MapReports(this IEndpointRouteBuilder app) =>
        app.MapGenerateNewPassesRegistrationsPerMonthReport();
}
