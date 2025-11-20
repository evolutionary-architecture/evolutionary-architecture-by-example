namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using Npgsql;

internal sealed class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly string _connectionString;
    private NpgsqlConnection? _connection;
    
    public DatabaseConnectionFactory(IOptions<DatabaseOptions> databaseOptions, IConfiguration configuration)
    {
        // Try to get Aspire connection string first
        _connectionString = configuration.GetConnectionString("fitnet") ?? databaseOptions.Value.ConnectionString ?? throw new InvalidOperationException("Database connection string is not configured");
    }

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            return _connection;
        }

        _connection = new NpgsqlConnection(_connectionString);
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
