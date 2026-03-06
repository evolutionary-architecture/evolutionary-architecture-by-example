var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

var fitnetContractsDatabase = postgres.AddDatabase("fitnetcontractsdb", "fitnet");

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

builder.AddProject<Fitnet_Contracts>("fitnet-contracts")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetContractsDatabase, "Database__ConnectionString")
    .WithReference(rabbitmq, "EventBus__Uri")
    .WaitFor(postgres)
    .WaitFor(rabbitmq);

await builder.Build().RunAsync();
