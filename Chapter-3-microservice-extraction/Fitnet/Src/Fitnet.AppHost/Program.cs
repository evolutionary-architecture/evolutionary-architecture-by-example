using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

var fitnetDatabase = postgres.AddDatabase("fitnetsdb", "fitnet");

builder.AddProject<Fitnet>("fitnet-modular-monolith")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, "Database__ConnectionString")
    .WaitFor(postgres);

await builder.Build().RunAsync();
