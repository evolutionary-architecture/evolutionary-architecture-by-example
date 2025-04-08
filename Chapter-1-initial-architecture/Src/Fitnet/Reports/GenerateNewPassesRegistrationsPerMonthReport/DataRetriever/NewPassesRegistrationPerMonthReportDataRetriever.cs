namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport.DataRetriever;

using Dapper;
using Dtos;
using DataAccess;

internal sealed class NewPassesRegistrationPerMonthReportDataRetriever(IDatabaseConnectionFactory databaseConnectionFactory, TimeProvider timeProvider) : INewPassesRegistrationPerMonthReportDataRetriever
{
    public async Task<IReadOnlyCollection<NewPassesRegistrationsPerMonthDto>> GetReportDataAsync(CancellationToken cancellationToken = default)
    {
        using var connection = databaseConnectionFactory.Create();
        var query = $@"
        SELECT EXTRACT(MONTH FROM ""Passes"".""From"")::INTEGER AS ""{nameof(NewPassesRegistrationsPerMonthDto.MonthOrder)}"",
               to_char(""Passes"".""From"", 'Month') AS ""{nameof(NewPassesRegistrationsPerMonthDto.MonthName)}"",
               COUNT(*) AS ""{nameof(NewPassesRegistrationsPerMonthDto.RegisteredPasses)}""
        FROM ""Passes"".""Passes""
        WHERE EXTRACT(YEAR FROM ""Passes"".""From"") = '{timeProvider.GetUtcNow().Year}'
        GROUP BY ""{nameof(NewPassesRegistrationsPerMonthDto.MonthName)}"", ""{nameof(NewPassesRegistrationsPerMonthDto.MonthOrder)}""
        ORDER BY ""{nameof(NewPassesRegistrationsPerMonthDto.MonthOrder)}""";

        var queryDefinition = new CommandDefinition(query, cancellationToken: cancellationToken);
        var newPassesRegistrationsPerMonthDtos = await connection.QueryAsync<NewPassesRegistrationsPerMonthDto>(queryDefinition);

        return [.. newPassesRegistrationsPerMonthDtos];
    }
}
