namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.Options;
using System.Data;
using Npgsql;

internal sealed class DatabaseConnectionFactory(IOptions<DatabaseOptions> databaseOptions) : IDatabaseConnectionFactory
{
    private NpgsqlConnection? _connection;

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }

        _connection =
            new NpgsqlConnection(databaseOptions.Value.ConnectionString);
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
