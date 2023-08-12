using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Core.SystemClock;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure;
using EvolutionaryArchitecture.Fitnet.Contracts.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSystemClock();
builder.Services.AddFeatureManagement();

builder.Services.AddContractsApi(builder.Configuration);
builder.Services.AddCommonInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();
app.RegisterContractsApi();

app.Run();

namespace EvolutionaryArchitecture.Fitnet.Contracts
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}