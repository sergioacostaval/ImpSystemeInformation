using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using StocksInvesthink.Data;
using StocksInvesthink.Services;
using StocksInvesthink.Services.Facade;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Add services to the container
// ----------------------------

builder.Services.AddControllersWithViews();

// Configuration de l'authentification par cookies
// Ce mécanisme permet de garder la session de l'utilisateur après connexion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Page vers laquelle rediriger si l'utilisateur n'est pas connecté
        options.LoginPath = "/Account/Login";

        // Durée de validité du cookie d'authentification
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

// Singleton DatabaseManager
builder.Services.AddSingleton<IDatabaseManager, DatabaseManager>();

// Service d'import CSV
builder.Services.AddScoped<CsvImportService>();

// Enregistrement de la façade
builder.Services.AddScoped<IStockAnalysisFacade, StockAnalysisFacade>();

//Indicator Service
builder.Services.AddScoped<IndicatorService>();

//Signal Service
builder.Services.AddScoped<SignalService>();

// Configuration EF Core + SQLite
builder.Services.AddDbContext<StocksInvesthinkContext>((serviceProvider, options) =>
{
    var dbManager = serviceProvider.GetRequiredService<IDatabaseManager>();
    options.UseSqlite("Data Source=stocksinvesthink.db");
});

var app = builder.Build();

// ----------------------------
// Configure HTTP request pipeline
// ----------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Middleware responsable de vérifier l'identité de l'utilisateur
// Il lit le cookie et authentifie l'utilisateur pour chaque requête
app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();