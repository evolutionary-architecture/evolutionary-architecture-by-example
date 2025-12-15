using Projects;

var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();
var db = postgres.AddDatabase("fitnetsdb", "fitnet");

builder.AddProject<Fitnet>("fitnet")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithEnvironment("Passes__ConnectionStrings__Primary", db)
    .WithEnvironment("Contracts__ConnectionStrings__Primary", db)
    .WithEnvironment("Reports__ConnectionStrings__Primary", db)
    .WithEnvironment("Offers__ConnectionStrings__Primary", db)
    .WaitFor(postgres);

await builder.Build().RunAsync();
