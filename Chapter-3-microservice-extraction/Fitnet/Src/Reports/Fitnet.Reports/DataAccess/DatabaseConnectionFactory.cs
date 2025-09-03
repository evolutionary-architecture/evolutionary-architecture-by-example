namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using Npgsql;

internal sealed class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;
    private NpgsqlConnection? _connection;

    public DatabaseConnectionFactory(IOptions<DatabaseOptions> databaseOptions)
    {
        _databaseOptions = databaseOptions;
    }

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }

        _connection = new NpgsqlConnection(_databaseOptions.Value.ConnectionString);
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
