using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Clock;
using EvolutionaryArchitecture.Fitnet.Contracts.Api;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddContractsApi(builder.Configuration);

builder.Services.AddClock();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs/v1");
}

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
