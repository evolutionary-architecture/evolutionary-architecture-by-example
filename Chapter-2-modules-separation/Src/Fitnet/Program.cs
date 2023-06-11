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

builder.Services.AddContracts(builder.Configuration, Modules.Contracts.ToString());
builder.Services.AddPasses(builder.Configuration, Modules.Passes.ToString());
builder.Services.AddOffers(builder.Configuration, Modules.Offers.ToString());
builder.Services.AddReports(Modules.Reports.ToString());

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

app.RegisterContracts(Modules.Contracts.ToString());
app.RegisterPasses(Modules.Passes.ToString());
app.RegisterOffers(Modules.Offers.ToString());
app.RegisterReports( Modules.Reports.ToString());

app.Run();

namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}