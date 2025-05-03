using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhitePie.Services;
using WhitePie.WebApp.Data.Interfaces;
using WhitePie.WebApp.Data.Repositories;
using WhitePie.WebApp.Data;
using WhitePie.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Always add environment variables
builder.Configuration.AddEnvironmentVariables();

// In development, add User Secrets
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var configuration = builder.Configuration;

// Retrieve configuration values
var dbConnectionString = Environment.GetEnvironmentVariable("WhitePieDbConnectionString") ?? configuration["ConnectionStrings:DatabaseConnectionString"];
var dbDatabaseName = Environment.GetEnvironmentVariable("WhitePieDbName") ?? configuration["WhitePieDbSettings:DatabaseName"];

// Validate configuration values
if (string.IsNullOrWhiteSpace(dbConnectionString) || string.IsNullOrWhiteSpace(dbDatabaseName))
{
    throw new InvalidOperationException("Database configuration is missing. Please ensure 'whitePieDbConnectionString' and 'whitePieDbDatabaseName' are set.");
}


// Register services and configure the app
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new MongoDbContext(dbConnectionString, dbDatabaseName));
builder.Services.AddScoped<IMomentService, MomentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IMomentRepository, MomentRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define your endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();