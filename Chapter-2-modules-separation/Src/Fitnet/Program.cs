using EvolutionaryArchitecture.Fitnet;
using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.SystemClock;
using EvolutionaryArchitecture.Fitnet.Contracts.Api;
using EvolutionaryArchitecture.Fitnet.Offers.Api;
using EvolutionaryArchitecture.Fitnet.Passes.Api;
using EvolutionaryArchitecture.Fitnet.Reports;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFeatureManagement();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSystemClock();
builder.Services.AddEventBus();

builder.Services.AddContracts(builder.Configuration, Module.Contracts);
builder.Services.AddPasses(builder.Configuration, Module.Passes);
builder.Services.AddOffers(builder.Configuration, Module.Offers);
builder.Services.AddReports(Module.Reports);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.RegisterContracts(Module.Contracts);
app.RegisterPasses(Module.Passes);
app.RegisterOffers(Module.Offers);
app.RegisterReports(Module.Reports);

app.Run();

namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}