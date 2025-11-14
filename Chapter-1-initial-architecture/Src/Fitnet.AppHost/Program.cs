using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL with proper password configuration
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

// Add the database
var fitnetDatabase = postgres.AddDatabase("fitnetsdb", "fitnet");

// Add Fitnet project with proper connection string references for all modules
builder.AddProject<Fitnet>("fitnet")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, connectionName: "Passes")
    .WithReference(fitnetDatabase, connectionName: "Contracts")
    .WithReference(fitnetDatabase, connectionName: "Reports")
    .WithReference(fitnetDatabase, connectionName: "Offers")
    .WaitFor(postgres);

builder.Build().Run();
