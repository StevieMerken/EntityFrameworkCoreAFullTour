using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(ob =>
{
    ob.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sqliteDatabaseName = builder.Configuration.GetConnectionString("SqliteDatabaseConnectionString");
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Path.Combine(Environment.GetFolderPath(folder), "EntityFrameworkCore");
var dbPath = Path.Combine(path, sqliteDatabaseName);
var connectionString = $"Data Source={dbPath}";

builder.Services.AddDbContext<FootballLeagueDBContext>(options => {
    options.UseSqlite(connectionString, slqLiteoptions =>
    {
        slqLiteoptions.CommandTimeout(30);
    });
    //.UseLazyLoadingProxies()
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(Console.WriteLine, LogLevel.Information);

    if (!builder.Environment.IsProduction())
    {
        options.EnableSensitiveDataLogging();//OK in testing a project, but be careful about enabling this in production.
        options.EnableDetailedErrors();
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
