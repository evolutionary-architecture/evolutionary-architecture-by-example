using Projects;

var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

var fitnetDatabase = postgres.AddDatabase("fitnetsdb", "fitnet");
builder.AddProject<Fitnet>("fitnet")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, "Passes__ConnectionStrings")
    .WithReference(fitnetDatabase, "Contracts__ConnectionStrings")
    .WithReference(fitnetDatabase, "Reports__ConnectionStrings")
    .WithReference(fitnetDatabase, "Offers__ConnectionStrings")
    .WaitFor(postgres);

await builder.Build().RunAsync();
