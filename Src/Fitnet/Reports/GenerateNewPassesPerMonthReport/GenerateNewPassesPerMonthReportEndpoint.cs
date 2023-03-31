namespace SuperSimpleArchitecture.Fitnet.Passes.MarkPassAsExpired;

using Data.Database;
using Reports.DataAccess.DatabaseConnection;
using Shared.SystemClock;

internal static class GenerateNewPassesPerMonthReportEndpoint
{
    internal static void MapGenerateNewPassesPerMonthReport(this IEndpointRouteBuilder app) =>
        app.MapGet(ReportsApiPaths.GenerateNewReport, (Guid id, IDatabaseConnectionFactory persistence, ISystemClock systemClock) =>
        {
            throw new NotImplementedException();
        });
}