using EvolutionaryArchitecture.Fitnet.Contracts;
using EvolutionaryArchitecture.Fitnet.Offers;
using EvolutionaryArchitecture.Fitnet.Passes;
using EvolutionaryArchitecture.Fitnet.Reports;
using EvolutionaryArchitecture.Fitnet.Shared.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Shared.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Shared.SystemClock;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSystemClock();
builder.Services.AddEventBus();

builder.Services.AddPasses(builder.Configuration);
builder.Services.AddContracts(builder.Configuration);
builder.Services.AddOffers(builder.Configuration);
builder.Services.AddReports();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.Run();

namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}