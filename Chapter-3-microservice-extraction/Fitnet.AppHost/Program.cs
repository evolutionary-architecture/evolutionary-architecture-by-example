using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

var fitnetDatabase = postgres.AddDatabase("fitnetsdb", "fitnet");

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

builder.AddProject<Fitnet>("fitnet-modular-monolith")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, "Database__ConnectionString")
    .WithReference(rabbitmq, "EventBus__ConnectionString")
    .WaitFor(postgres)
    .WaitFor(rabbitmq);

builder.AddProject<Fitnet_Contracts>("fitnet-contracts-microservice")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, "Database__ConnectionString")
    .WithReference(rabbitmq, "EventBus__ConnectionString")
    .WaitFor(postgres)
    .WaitFor(rabbitmq);

await builder.Build().RunAsync();
