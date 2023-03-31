namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport;

using SuperSimpleArchitecture.Fitnet.Shared.SystemClock;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesPerMonthReport(this IEndpointRouteBuilder app) =>
        app.MapGet(ReportsApiPaths.GenerateNewReport, (Guid id, ISystemClock systemClock) =>
        {
            throw new NotImplementedException();
        });
}