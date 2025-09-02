namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using System.Data;
using Npgsql;

internal sealed class DatabaseConnectionFactory(IConfiguration configuration) : IDatabaseConnectionFactory
{
    private NpgsqlConnection? _connection;

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }
        _connection =
            new NpgsqlConnection(configuration.GetConnectionString("Reports"));
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
