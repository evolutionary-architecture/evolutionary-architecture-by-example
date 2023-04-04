namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.DataRetriver;

using Dtos;

internal interface INewPassesPerMonthReportDataRetriever
{
   Task<IReadOnlyCollection<NewPassesPerMonthDto>> GetReportDataAsync();
}