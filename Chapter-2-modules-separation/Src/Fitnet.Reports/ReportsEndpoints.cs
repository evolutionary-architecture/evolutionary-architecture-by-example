namespace EvolutionaryArchitecture.Fitnet.Reports;

using Microsoft.AspNetCore.Routing;
using GenerateNewPassesRegistrationsPerMonthReport;

public static class ReportsEndpoints
{
    public static void MapReports(this IEndpointRouteBuilder app) => 
        app.MapGenerateNewPassesRegistrationsPerMonthReport();
}