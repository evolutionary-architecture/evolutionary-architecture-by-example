using Projects;

var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres", "14.3")
    .WithPgAdmin();

var fitnetDatabase = postgres.AddDatabase("fitnetsdb", "fitnet");
builder.AddProject<Fitnet>("fitnet")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(fitnetDatabase, "Modules__Passes__ConnectionStrings__Primary")
    .WithReference(fitnetDatabase, "Modules__Contracts__ConnectionStrings__Primary")
    .WithReference(fitnetDatabase, "Modules__Reports__ConnectionStrings__Primary")
    .WithReference(fitnetDatabase, "Modules__Offers__ConnectionStrings__Primary")
    .WaitFor(postgres);

await builder.Build().RunAsync();
