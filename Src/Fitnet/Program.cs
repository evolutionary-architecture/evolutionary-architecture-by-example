using SuperSimpleArchitecture.Fitnet.Contracts;
using SuperSimpleArchitecture.Fitnet.Passes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPasses(builder.Configuration);
builder.Services.AddContracts(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePasses();
app.UseContracts();
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPasses();
app.MapContracts();

app.Run();

namespace SuperSimpleArchitecture.Fitnet
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}