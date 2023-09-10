namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport.DataRetriever;

using Dtos;

internal interface INewPassesRegistrationPerMonthReportDataRetriever
{
    Task<IReadOnlyCollection<NewPassesRegistrationsPerMonthDto>> GetReportDataAsync(CancellationToken cancellationToken = default);
}
