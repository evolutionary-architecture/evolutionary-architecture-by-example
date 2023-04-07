namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport;

using DataRetriver;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesPerMonthReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ReportsApiPaths.GenerateNewReport,  async (
            INewPassesPerMonthReportDataRetriever dataRetriever) =>
        {
            var reportData = await dataRetriever.GetReportDataAsync();

            return Results.Ok(reportData);
        });
    }
}