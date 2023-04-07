namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport.DataRetriever;

using Dapper;
using Dtos;
using SuperSimpleArchitecture.Fitnet.Reports.DataAccess;
using SuperSimpleArchitecture.Fitnet.Shared.SystemClock;

internal sealed class NewPassesRegistrationPerMonthReportDataRetriever : INewPassesRegistrationPerMonthReportDataRetriever
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
    private readonly ISystemClock _clock;

    public NewPassesRegistrationPerMonthReportDataRetriever(IDatabaseConnectionFactory databaseConnectionFactory, ISystemClock clock)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
        _clock = clock;
    }

    public async Task<IReadOnlyCollection<NewPassesRegistrationsPerMonthDto>> GetReportDataAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _databaseConnectionFactory.Create();
        var query = $@"
        SELECT to_char(""Passes"".""From"", 'Month') AS {nameof(NewPassesRegistrationsPerMonthDto.Month)},
               COUNT(*) AS {nameof(NewPassesRegistrationsPerMonthDto.RegisteredPasses)}
        FROM ""Passes"".""Passes""
        WHERE EXTRACT(YEAR FROM ""Passes"".""From"") = '{_clock.Now.Year}'
        GROUP BY {nameof(NewPassesRegistrationsPerMonthDto.Month)}";   
        
        var newPassesRegistrationsPerMonthDtos = await connection.QueryAsync<NewPassesRegistrationsPerMonthDto>(query);

        return newPassesRegistrationsPerMonthDtos.ToList();
    }
}