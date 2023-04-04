namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.DataRetriver;

using System.Data;
using Npgsql;

internal sealed class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly IConfiguration _configuration;
    private IDbConnection? _connection;

    public DatabaseConnectionFactory(IConfiguration configuration) => _configuration = configuration;

    public IDbConnection Create()
    {
        if (_connection is { State: ConnectionState.Open })
            return _connection;

        _connection =
            new NpgsqlConnection(_configuration.GetConnectionString("Reports"));
        _connection.Open();

        return _connection;
    }

    public void Dispose()
    {
        if (_connection is { State: ConnectionState.Open }) _connection.Dispose();
    }
}