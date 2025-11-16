using Projects;

var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

var fitnetDatabase = postgres.AddDatabase("fitnetsdb", "fitnet");
builder.AddProject<Fitnet>("fitnet")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, "Passes")
    .WithReference(fitnetDatabase, "Contracts")
    .WithReference(fitnetDatabase, "Reports")
    .WithReference(fitnetDatabase, "Offers")
    .WaitFor(postgres);

builder.Build().Run();
