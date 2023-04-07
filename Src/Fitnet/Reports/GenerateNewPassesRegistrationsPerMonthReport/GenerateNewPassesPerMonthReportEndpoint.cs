namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using DataRetriever;
using Dtos;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesRegistrationsPerMonthReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ReportsApiPaths.GenerateNewReport,  async (
            INewPassesRegistrationPerMonthReportDataRetriever dataRetriever) =>
        {
            var reportData = await dataRetriever.GetReportDataAsync();
            var newPassesRegistrationsPerMonthResponse = NewPassesRegistrationsPerMonthResponse.Create(reportData);
            
            return Results.Ok(newPassesRegistrationsPerMonthResponse);
        });
    }
}