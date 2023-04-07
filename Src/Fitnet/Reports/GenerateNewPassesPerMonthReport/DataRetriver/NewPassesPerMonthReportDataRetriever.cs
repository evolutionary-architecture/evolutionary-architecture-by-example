namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.DataRetriver;

using Dapper;
using Dtos;
using Shared.SystemClock;

internal sealed class NewPassesPerMonthReportDataRetriever : INewPassesPerMonthReportDataRetriever
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
    private readonly ISystemClock _clock;

    public NewPassesPerMonthReportDataRetriever(IDatabaseConnectionFactory databaseConnectionFactory, ISystemClock clock)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
        _clock = clock;
    }

    public async Task<IReadOnlyCollection<NewPassesPerMonthDto>> GetReportDataAsync()
    {
        using var connection = _databaseConnectionFactory.Create();
        var query = $@"
        SELECT to_char(""Passes"".""From"", 'Month') AS {nameof(NewPassesPerMonthDto.Month)},
               COUNT(*) AS {nameof(NewPassesPerMonthDto.RegisteredPasses)}
        FROM ""Passes"".""Passes""
        WHERE EXTRACT(YEAR FROM ""Passes"".""From"") = '{_clock.Now.Year}'
        GROUP BY {nameof(NewPassesPerMonthDto.Month)}";   
        
        var passes = await connection.QueryAsync<NewPassesPerMonthDto>(query);

        return passes.ToList();
    }
}