using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Infastrcuture;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Repositories.PorductRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(op=>op.SerializerSettings.ReferenceLoopHandling=
Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<CandelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddScoped<IproductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<CandelContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAync(context);
}
catch (System.Exception ex)
{
    logger.LogError(ex, "error occured during migration");
}

app.Run();
