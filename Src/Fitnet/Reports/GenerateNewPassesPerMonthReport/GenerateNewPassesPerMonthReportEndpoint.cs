namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport;

using System.Net.Mime;
using DataRetriver;
using ReportGenerator;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    private const string ReportName = "new_passes_per_month.pdf";
    
    internal static void MapGenerateNewPassesPerMonthReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ReportsApiPaths.GenerateNewReport,  async (
            INewPassesPerMonthReportDataPdfReportGenerator generator, 
            INewPassesPerMonthReportDataRetriever dataRetriever) =>
        {
            var reportData = await dataRetriever.GetReportDataAsync();
            var report = await generator.GeneratePdfReportAsync(ReportName, reportData);

            return Results.File(report, MediaTypeNames.Application.Pdf, ReportName);
        });
    }
}