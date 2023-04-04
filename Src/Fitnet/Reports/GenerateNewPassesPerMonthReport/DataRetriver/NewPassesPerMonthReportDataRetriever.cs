namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.DataRetriver;

using Dapper;
using Dtos;

internal sealed class NewPassesPerMonthReportDataRetriever : INewPassesPerMonthReportDataRetriever
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

    public NewPassesPerMonthReportDataRetriever(IDatabaseConnectionFactory databaseConnectionFactory) => 
        _databaseConnectionFactory = databaseConnectionFactory;

    public async Task<IReadOnlyCollection<NewPassesPerMonthDto>> GetReportDataAsync()
    {
        using var connection = _databaseConnectionFactory.Create();
        const string query = $@"
        SELECT to_char(""Passes"".""From"", 'Month') AS {nameof(NewPassesPerMonthDto.Month)},
               COUNT(*) AS {nameof(NewPassesPerMonthDto.RegisteredPasses)}
        FROM ""Passes"".""Passes""
        WHERE EXTRACT(YEAR FROM ""Passes"".""From"") = EXTRACT(YEAR FROM NOW())
        GROUP BY {nameof(NewPassesPerMonthDto.Month)}";   
        
        var passes = await connection.QueryAsync<NewPassesPerMonthDto>(query);

        return passes.ToList();
    }
}