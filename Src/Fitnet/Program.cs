using SuperSimpleArchitecture.Fitnet.Contracts;
using SuperSimpleArchitecture.Fitnet.Passes;
using SuperSimpleArchitecture.Fitnet.Reports;
using SuperSimpleArchitecture.Fitnet.Shared.Events.EventBus;
using SuperSimpleArchitecture.Fitnet.Shared.SystemClock;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSystemClock();
builder.Services.AddEventBus();

builder.Services.AddPasses(builder.Configuration);
builder.Services.AddContracts(builder.Configuration);
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
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPasses();
app.MapContracts();
app.MapReports();

app.Run();

namespace SuperSimpleArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}