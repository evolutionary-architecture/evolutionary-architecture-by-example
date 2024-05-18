namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Npgsql;

internal sealed class DatabaseConnectionFactory(IConfiguration configuration) : IDatabaseConnectionFactory
{
    private const string ConnectionStringConfigurationSection = "Modules:Reports:ConnectionStrings:Primary";
    private NpgsqlConnection? _connection;

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }

        var connectionString = configuration.GetRequiredSection(ConnectionStringConfigurationSection).Value;
        _connection =
            new NpgsqlConnection(connectionString);
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
