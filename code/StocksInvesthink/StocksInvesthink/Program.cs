using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Add services to the container
// ----------------------------

builder.Services.AddControllersWithViews();

// Singleton DatabaseManager
builder.Services.AddSingleton<IDatabaseManager, DatabaseManager>();

//Csv Import Service
builder.Services.AddScoped<CsvImportService>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
