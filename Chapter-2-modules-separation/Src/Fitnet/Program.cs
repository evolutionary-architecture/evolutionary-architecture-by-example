using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Core.SystemClock;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure;
using EvolutionaryArchitecture.Fitnet.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSystemClock();
builder.Services.AddExceptionHandling();
builder.Services.AddCommonInfrastructure();
builder.Services.AddModules(builder.Configuration);

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

app.RegisterModules();

app.Run();


namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program;
}
