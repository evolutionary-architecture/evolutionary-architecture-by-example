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
        SELECT DATENAME(Month, From)) AS MonthName,
               COUNT(*) AS NewPassesCount
        FROM Passes 
        WHERE 'From' >= @StartDate 
          AND 'To' <= @EndDate
        GROUP BY DATENAME(Month, From)";    
        
        var passes = await connection.QueryAsync<NewPassesPerMonthDto>(query, 
            new
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 
                    DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
            });

        return passes.ToList();
    }
}