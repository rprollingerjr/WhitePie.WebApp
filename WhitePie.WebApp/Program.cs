using Microsoft.AspNetCore.Authentication.Certificate;
using WhitePie.Models.Settings;
using WhitePie.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Certificate 
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();
    builder.Services.Configure<WhitePieDatabaseSettings>(
    builder.Environment.IsDevelopment() ?
    builder.Configuration.GetSection("DevelopmentWhitePieDatabase") :
    builder.Configuration.GetSection("WhitePieDatabase"));

builder.Services.AddSingleton<MomentsService>();
builder.Services.AddSingleton<EventsService>();

var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
