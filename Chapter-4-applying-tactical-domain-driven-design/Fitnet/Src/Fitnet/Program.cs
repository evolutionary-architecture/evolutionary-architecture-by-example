using EvolutionaryArchitecture.Fitnet;
using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Clock;
using EvolutionaryArchitecture.Fitnet.Offers.Api;
using EvolutionaryArchitecture.Fitnet.Passes.Api;
using EvolutionaryArchitecture.Fitnet.Reports;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddClock();

builder.Services.AddPasses(builder.Configuration, Module.Passes);
builder.Services.AddOffers(builder.Configuration, Module.Offers);
builder.Services.AddReports(builder.Configuration, Module.Reports);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs/v1");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.RegisterPasses(Module.Passes);
app.RegisterOffers(Module.Offers);
app.RegisterReports(Module.Reports);

await app.RunAsync();

namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program;
}
