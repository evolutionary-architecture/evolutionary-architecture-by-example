var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL database
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres")
    .WithImageTag("14.3")
    .WithEnvironment("POSTGRES_PASSWORD", "mysecretpassword")
    .WithHealthCheck();

var fitnetDb = postgres.AddDatabase("fitnet");

// Add RabbitMQ message broker
var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithImage("rabbitmq")
    .WithImageTag("management")
    .WithManagementPlugin()
    .WithHealthCheck();

// Add Fitnet Contracts Microservice
var contractsApi = builder.AddProject<Projects.Fitnet_Contracts>("fitnet-contracts-microservice")
    .WithReference(fitnetDb)
    .WithReference(rabbitmq)
    .WithHttpEndpoint(port: 8081, targetPort: 80, name: "http")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development");

// Add Fitnet Modular Monolith
var fitnetApi = builder.AddProject<Projects.Fitnet>("fitnet-modular-monolith")
    .WithReference(fitnetDb)
    .WithReference(rabbitmq)
    .WithHttpEndpoint(port: 8080, targetPort: 80, name: "http")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development");

builder.Build().Run();
