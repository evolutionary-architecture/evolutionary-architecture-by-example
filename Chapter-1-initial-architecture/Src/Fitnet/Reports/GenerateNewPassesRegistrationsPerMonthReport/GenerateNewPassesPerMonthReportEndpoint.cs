namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

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
        .WithSummary("Returns report of all passes registered in a month")
        .WithDescription("This endpoint is used to retrieve all passes that were registered in a given month.")
        .Produces<NewPassesRegistrationsPerMonthResponse>()
        .Produces(StatusCodes.Status500InternalServerError);
}
