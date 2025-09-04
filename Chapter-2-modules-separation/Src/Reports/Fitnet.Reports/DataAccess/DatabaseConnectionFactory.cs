namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Npgsql;

internal sealed class DatabaseConnectionFactory(IOptions<ReportsPersistenceOptions> persistenceOptions) : IDatabaseConnectionFactory
{
    private NpgsqlConnection? _connection;

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }

        var connectionString = persistenceOptions.Value.Primary;
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();

        return _connection;
    }

    [ExcludeFromCodeCoverage]
    public void Dispose()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            _connection.Dispose();
        }
    }
}
