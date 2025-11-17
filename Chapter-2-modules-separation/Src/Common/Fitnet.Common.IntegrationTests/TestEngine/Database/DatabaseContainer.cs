namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;

using Testcontainers.PostgreSql;

[UsedImplicitly]
public sealed class DatabaseContainer : IAsyncLifetime
{
    private const string Username = "admin";
    private const string Password = "$3cureP@ssw0rd";
    private const string Database = "fitnet";
    private PostgreSqlContainer? _container;
    public string? ConnectionString { get; private set; }

    public async ValueTask InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithDatabase(Database)
            .WithUsername(Username)
            .WithPassword(Password)
            .Build();

        await _container!.StartAsync();

        ConnectionString = _container.GetConnectionString();
    }

    public async ValueTask DisposeAsync() => await _container!.StopAsync();
}
