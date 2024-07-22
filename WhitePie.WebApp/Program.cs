using Microsoft.AspNetCore.Authentication.Certificate;
using WhitePie.Services;
using WhitePie.WebApp.Data;
using WhitePie.WebApp.Data.Interfaces;
using WhitePie.WebApp.Data.Repositories;
using WhitePie.WebApp.Services.Interfaces;
using WhitePie.WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Certificate 
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

string whitePieDbConnectionString;
string whitePieDbDatabaseName;
if (string.Equals(builder.Environment.EnvironmentName, "Development"))
{
    whitePieDbConnectionString = builder.Configuration["ConnectionStrings:DatabaseConnectionString"];
    whitePieDbDatabaseName = builder.Configuration["WhitePieDbSettings:DatabaseName"];
}
else
{
    whitePieDbConnectionString = Environment.GetEnvironmentVariable("MongoDb_ConnectionString");
    whitePieDbDatabaseName = Environment.GetEnvironmentVariable("MongoDb_DatabaseName");
}

builder.Services.AddSingleton(new MongoDbContext(whitePieDbConnectionString, whitePieDbDatabaseName));
builder.Services.AddScoped<IMomentService, MomentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IMomentRepository, MomentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<MaintenanceMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
