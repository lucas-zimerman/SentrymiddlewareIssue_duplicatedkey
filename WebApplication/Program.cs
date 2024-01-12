using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject1")]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSentry();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSentryTracing();

app.UseAuthorization();

app.MapControllers();

app.Run();
