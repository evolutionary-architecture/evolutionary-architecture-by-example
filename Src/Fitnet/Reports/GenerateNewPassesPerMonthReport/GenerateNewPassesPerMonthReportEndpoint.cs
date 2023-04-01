namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport;

using System.Net.Mime;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesPerMonthReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ReportsApiPaths.GenerateNewReport, () =>
        {
            var fakerReportFile = Enumerable.Repeat((byte)0x20, 100).ToArray();

            return Results.File(fakerReportFile, MediaTypeNames.Application.Pdf);
        });
    }
}