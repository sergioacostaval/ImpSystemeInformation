using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Services.Facade;
using StocksInvesthink.ViewModels;
using System.Security.Claims;

namespace StocksInvesthink.Controllers
{
    // L'utilisateur doit être authentifié pour accéder à l'analyse
    [Authorize]
    public class AnalysisController : Controller
    {
        private readonly StocksInvesthinkContext _db;
        private readonly IStockAnalysisFacade _analysisFacade;

        // Injection du DbContext et de la façade d'analyse
        public AnalysisController(StocksInvesthinkContext db, IStockAnalysisFacade analysisFacade)
        {
            _db = db;
            _analysisFacade = analysisFacade;
        }

        // Page pour importer un fichier CSV
        public async Task<IActionResult> Import()
        {
            // Charger les stocks disponibles
            ViewBag.Stocks = await _db.Stocks.ToListAsync();

            return View();
        }

        // Traitement du formulaire CSV
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file, int stockId)
        {
            // Récupération de l'utilisateur connecté depuis le cookie
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Lancer l'analyse via la façade
            int rows = await _analysisFacade.RunFullAnalysisAsync(file, userId, stockId);

            TempData["Message"] = $"Import terminé : {rows} lignes ajoutées";

            return RedirectToAction("Import");
        }

        // methode pour afficher les resultats de analyse
        public async Task<IActionResult> Results(int stockId)
        {
            // recuperer utilisateur connecte
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // recuperer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            // recuperer toutes les valeurs indicateurs
            var indicatorValues = await _db.IndicatorValues
                .Include(v => v.IndicatorInstance)
                .ThenInclude(i => i.IndicatorType)
                .Where(v => v.IndicatorInstance.UserId == userId &&
                            v.IndicatorInstance.StockId == stockId)
                .ToListAsync();

            // liste finale pour la vue
            var results = new List<AnalysisResultViewModel>();

            // construire chaque ligne du resultat
            foreach (var price in prices)
            {
                // chercher SMA pour la date
                var sma = indicatorValues
                    .FirstOrDefault(v =>
                        v.Date == price.Date &&
                        v.IndicatorInstance.IndicatorType.Name == "SMA");

                // chercher EMA pour la date
                var ema = indicatorValues
                    .FirstOrDefault(v =>
                        v.Date == price.Date &&
                        v.IndicatorInstance.IndicatorType.Name == "EMA");

                // chercher RSI pour la date
                var rsi = indicatorValues
                    .FirstOrDefault(v =>
                        v.Date == price.Date &&
                        v.IndicatorInstance.IndicatorType.Name == "RSI");

                // ajouter ligne resultat
                results.Add(new AnalysisResultViewModel(
                    price.Date,
                    price.ClosePrice,
                    sma?.Value,
                    ema?.Value,
                    rsi?.Value
                ));
            }

            // envoyer resultat vers la vue
            return View(results);
        }
    }
}