namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Database;

using Testcontainers.PostgreSql;

[UsedImplicitly]
public sealed class DatabaseContainer : IAsyncLifetime
{
    private const string Username = "admin";
    private const string Password = "$3cureP@ssw0rd";
    private const string Database = "fitnet";
    private PostgreSqlContainer? _container;
    public string? ConnectionString { get; set; }

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithDatabase(Database)
            .WithUsername(Username)
            .WithPassword(Password)
            .Build();

        await _container!.StartAsync();

        ConnectionString = _container.GetConnectionString();
    }

    public async Task DisposeAsync() => await _container!.StopAsync();
}
