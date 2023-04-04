namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.DataRetriver;

using System.Data;

internal interface IDatabaseConnectionFactory : IDisposable
{
    IDbConnection Create();
}
