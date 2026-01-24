using EvolutionaryArchitecture.Fitnet.Common.Clock;
using EvolutionaryArchitecture.Fitnet.Common.Documentation;
using EvolutionaryArchitecture.Fitnet.Common.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.Validation.Requests;
using EvolutionaryArchitecture.Fitnet.Contracts;
using EvolutionaryArchitecture.Fitnet.Offers;
using EvolutionaryArchitecture.Fitnet.Passes;
using EvolutionaryArchitecture.Fitnet.Reports;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddExceptionHandling();
builder.Services.AddOpenApi();
builder.Services.AddEventBus();
builder.Services.AddRequestsValidations();
builder.Services.AddClock();

builder.Services.AddPasses(builder.Configuration);
builder.Services.AddContracts(builder.Configuration);
builder.Services.AddOffers(builder.Configuration);
builder.Services.AddReports(builder.Configuration);

await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseApiDocumentation();
app.UsePasses();
app.UseContracts();
app.UseReports();
app.UseOffers();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.MapPasses();
app.MapContracts();
app.MapReports();

await app.RunAsync();

namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program;
}
