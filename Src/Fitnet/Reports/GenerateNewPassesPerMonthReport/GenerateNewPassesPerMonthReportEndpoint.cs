namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport;

using System.Net.Mime;
using DataRetriver;
using ReportGenerator;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesPerMonthReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ReportsApiPaths.GenerateNewReport,  async (
            INewPassesPerMonthReportDataPdfReportGenerator generator, 
            INewPassesPerMonthReportDataRetriever dataRetriever) =>
        {
            var data = await dataRetriever.GetReportDataAsync();
            var fakerReportFile = await generator.GeneratePdfReportAsync("test", data);

            return Results.File(fakerReportFile, MediaTypeNames.Application.Pdf, "test.pdf");
        });
    }
}