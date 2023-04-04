namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.ReportGenerator;

using Dtos;

interface INewPassesPerMonthReportDataPdfReportGenerator
{
    Task<FileStream> GeneratePdfReportAsync(string name, IReadOnlyCollection<NewPassesPerMonthDto> report, CancellationToken cancellationToken = default);
}