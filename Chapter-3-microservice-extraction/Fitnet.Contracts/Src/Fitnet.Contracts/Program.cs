using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Clock;
using EvolutionaryArchitecture.Fitnet.Contracts.Api;
using Fitnet.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddContractsApi(builder.Configuration);

builder.Services.AddClock();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();
app.RegisterContractsApi();

await app.RunAsync();

namespace EvolutionaryArchitecture.Fitnet.Contracts
{
    [UsedImplicitly]
    public sealed class Program;
}
