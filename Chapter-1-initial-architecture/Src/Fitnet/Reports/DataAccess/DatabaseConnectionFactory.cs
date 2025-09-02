namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

internal sealed class DatabaseConnectionFactory(IOptions<ReportsPersistenceOptions> persistenceOptions)
    : IDatabaseConnectionFactory
{
    private NpgsqlConnection? _connection;

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }

        _connection = new NpgsqlConnection(persistenceOptions.Value.Reports);
        _connection.Open();

        return _connection;
    }

    public void Dispose()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            _connection.Dispose();
        }
    }
}
