using EvolutionaryArchitecture.Fitnet;
using EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Core.SystemClock;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;
using EvolutionaryArchitecture.Fitnet.Contracts.Api;
using EvolutionaryArchitecture.Fitnet.Offers.Api;
using EvolutionaryArchitecture.Fitnet.Passes.Api;
using EvolutionaryArchitecture.Fitnet.Reports;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSystemClock();
builder.Services.AddExceptionHandling();
builder.Services.AddCommonInfrastructure();

RegisterModules(builder);

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

app.RegisterContracts(Module.Contracts);
app.RegisterPasses(Module.Passes);
app.RegisterOffers(Module.Offers);
app.RegisterReports(Module.Reports);

app.Run();

static void RegisterModules(WebApplicationBuilder webApplicationBuilder)
{
    using var moduleAvailabilityChecker = ModuleAvailabilityChecker.Build(webApplicationBuilder.Configuration);
    webApplicationBuilder.Services.AddContracts(Module.Contracts, webApplicationBuilder.Configuration, moduleAvailabilityChecker);
    webApplicationBuilder.Services.AddPasses(Module.Passes, webApplicationBuilder.Configuration, moduleAvailabilityChecker);
    webApplicationBuilder.Services.AddOffers(Module.Offers, webApplicationBuilder.Configuration, moduleAvailabilityChecker);
    webApplicationBuilder.Services.AddReports(Module.Reports, moduleAvailabilityChecker);
}

namespace EvolutionaryArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program;
}
