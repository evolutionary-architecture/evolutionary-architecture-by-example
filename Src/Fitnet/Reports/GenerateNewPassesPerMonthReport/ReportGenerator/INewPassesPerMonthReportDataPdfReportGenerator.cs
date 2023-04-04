namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.ReportGenerator;

using Dtos;

interface INewPassesPerMonthReportDataPdfReportGenerator
{
    Task<byte[]> GeneratePdfReportAsync(string name, IReadOnlyCollection<NewPassesPerMonthDto> report, CancellationToken cancellationToken = default);
}