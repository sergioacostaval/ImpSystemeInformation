using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Services;
using StocksInvesthink.Services.Commands;
using StocksInvesthink.Services.Facade;

var builder = WebApplication.CreateBuilder(args); //main

// Add services to the container

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddSingleton<IDatabaseManager, DatabaseManager>();
builder.Services.AddScoped<CsvImportService>();
builder.Services.AddScoped<AnalysisCommandInvoker>();
builder.Services.AddScoped<IStockAnalysisFacade, StockAnalysisFacade>();
builder.Services.AddScoped<IndicatorService>();
builder.Services.AddScoped<SignalService>();

builder.Services.AddDbContext<StocksInvesthinkContext>((serviceProvider, options) =>
{
    var dbManager = serviceProvider.GetRequiredService<IDatabaseManager>();
    options.UseSqlite("Data Source=stocksinvesthink.db");
});

var app = builder.Build();

// Configure HTTP request pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();