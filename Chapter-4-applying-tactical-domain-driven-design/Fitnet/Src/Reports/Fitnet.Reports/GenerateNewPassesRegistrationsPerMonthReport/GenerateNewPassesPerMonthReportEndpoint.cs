namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using DataRetriever;
using Dtos;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesRegistrationsPerMonthReport(this IEndpointRouteBuilder app) => app.MapGet(
            ReportsApiPaths.GenerateNewReport, async (
                INewPassesRegistrationPerMonthReportDataRetriever dataRetriever,
                CancellationToken cancellationToken) =>
            {
                var reportData = await dataRetriever.GetReportDataAsync(cancellationToken);
                var newPassesRegistrationsPerMonthResponse = NewPassesRegistrationsPerMonthResponse.Create(reportData);

                return Results.Ok(newPassesRegistrationsPerMonthResponse);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Returns report of all passes registered in a month",
            Description =
                "This endpoint is used to retrieve all passes that were registered in a given month."
        })
        .Produces<NewPassesRegistrationsPerMonthResponse>()
        .Produces(StatusCodes.Status500InternalServerError);
}
