using EvolutionaryArchitecture.Fitnet.Common.Api.Documentation;
using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure;
using EvolutionaryArchitecture.Fitnet.Modules;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandling();
builder.Services.AddCommonInfrastructure();
builder.Services.AddModules(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs/v1");
}

app.UseApiDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();

app.RegisterModules();

await app.RunAsync();


namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program;
}
