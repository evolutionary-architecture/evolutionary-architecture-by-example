namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using DataRetriever;
using Dtos;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesRegistrationsPerMonthReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ReportsApiPaths.GenerateNewReport,async (
            INewPassesRegistrationPerMonthReportDataRetriever dataRetriever, 
            CancellationToken cancellationToken) =>
        {
            var reportData = await dataRetriever.GetReportDataAsync(cancellationToken);
            var newPassesRegistrationsPerMonthResponse = NewPassesRegistrationsPerMonthResponse.Create(reportData);
            
            return Results.Ok(newPassesRegistrationsPerMonthResponse);
        });
    }
}