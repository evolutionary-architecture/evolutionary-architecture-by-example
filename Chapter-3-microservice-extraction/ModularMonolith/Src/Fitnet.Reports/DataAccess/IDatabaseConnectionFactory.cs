namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using System.Data;

internal interface IDatabaseConnectionFactory : IDisposable
{
    IDbConnection Create();
}
