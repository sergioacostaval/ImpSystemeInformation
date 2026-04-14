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

        // Inclusion de DbContext et de la façade d'analyse
        public AnalysisController(StocksInvesthinkContext db, IStockAnalysisFacade analysisFacade)
        {
            _db = db;
            _analysisFacade = analysisFacade;
        }

        // Page pour importer un fichier CSV
        public async Task<IActionResult> Import()
        {
            ViewBag.Stocks = await _db.Stocks.ToListAsync();
            return View();
        }

        // Traitement du formulaire CSV
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file, int stockId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int rows = await _analysisFacade.RunFullAnalysisAsync(file, userId, stockId);

            TempData["Message"] = $"Import terminé : {rows} lignes ajoutées";
            return RedirectToAction("Import");
        }

        // Afficher les resultats de analyse
        public async Task<IActionResult> Results(int stockId)
        {
            // recuperer utilisateur connecte
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // recuperer le stock selectionne
            var stock = await _db.Stocks.FirstOrDefaultAsync(s => s.StockId == stockId);

            if (stock == null)
            {
                return NotFound();
            }

            // recuperer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            if (!prices.Any())
            {
                return View(new List<AnalysisResultViewModel>());
            }

            // recuperer toutes les valeurs indicateurs
            var indicatorValues = await _db.IndicatorValues
                .Include(v => v.IndicatorInstance)
                .ThenInclude(i => i.IndicatorType)
                .Where(v => v.IndicatorInstance.UserId == userId &&
                            v.IndicatorInstance.StockId == stockId)
                .ToListAsync();

            // recuperer tous les signaux pour ce user et ce stock
            var signals = await _db.Signals
                .Include(s => s.IndicatorValue)
                .ThenInclude(v => v.IndicatorInstance)
                .Where(s => s.IndicatorValue.IndicatorInstance.UserId == userId &&
                            s.IndicatorValue.IndicatorInstance.StockId == stockId)
                .OrderBy(s => s.Date)
                .ToListAsync();

            // date la plus recente de l'analyse
            var latestAnalysisDate = prices.Last().Date.Date;

            // fenetre de recence de 15 jours
            var recentLimitDate = latestAnalysisDate.AddDays(-15);

            // chercher la derniere signal recente pour chaque indicateur
            var lastRecentSmaSignal = signals
                .Where(s => s.Date.Date >= recentLimitDate &&
                            s.Date.Date <= latestAnalysisDate &&
                            (s.Type == "Buy" || s.Type == "Sell"))
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();

            var lastRecentEmaSignal = signals
                .Where(s => s.Date.Date >= recentLimitDate &&
                            s.Date.Date <= latestAnalysisDate &&
                            (s.Type == "Buy EMA" || s.Type == "Sell EMA"))
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();

            var lastRecentRsiSignal = signals
                .Where(s => s.Date.Date >= recentLimitDate &&
                            s.Date.Date <= latestAnalysisDate &&
                            (s.Type == "Buy RSI" || s.Type == "Sell RSI"))
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();

            // calculer score global
            int score = 0;

            // SMA
            if (lastRecentSmaSignal?.Type.Contains("Buy") == true) score++;
            if (lastRecentSmaSignal?.Type.Contains("Sell") == true) score--;

            // EMA
            if (lastRecentEmaSignal?.Type.Contains("Buy") == true) score++;
            if (lastRecentEmaSignal?.Type.Contains("Sell") == true) score--;

            // RSI
            if (lastRecentRsiSignal?.Type.Contains("Buy") == true) score++;
            if (lastRecentRsiSignal?.Type.Contains("Sell") == true) score--;

            //temp
            Console.WriteLine($"Latest analysis date: {latestAnalysisDate:yyyy-MM-dd}");
            Console.WriteLine($"Recent limit date: {recentLimitDate:yyyy-MM-dd}");
            Console.WriteLine($"Last SMA signal: {lastRecentSmaSignal?.Type} - {lastRecentSmaSignal?.Date:yyyy-MM-dd}");
            Console.WriteLine($"Last EMA signal: {lastRecentEmaSignal?.Type} - {lastRecentEmaSignal?.Date:yyyy-MM-dd}");
            Console.WriteLine($"Last RSI signal: {lastRecentRsiSignal?.Type} - {lastRecentRsiSignal?.Date:yyyy-MM-dd}");

            Console.WriteLine($"Score: {score}");

            // calcul de la recommandation globale
            string combinedSignal =
                score >= 3 ? "Strong Buy" :
                score == 2 ? "Buy" :
                score == -2 ? "Sell" :
                score <= -3 ? "Strong Sell" :
                "Hold";

            // liste finale pour la vue
            var results = new List<AnalysisResultViewModel>();

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

                // chercher signaux SMA pour la date
                var smaSignal = signals
                    .FirstOrDefault(s =>
                        s.Date.Date == price.Date.Date &&
                        (s.Type == "Buy" || s.Type == "Sell"));

                // chercher signaux EMA pour la date
                var emaSignal = signals
                    .FirstOrDefault(s =>
                        s.Date.Date == price.Date.Date &&
                        (s.Type == "Buy EMA" || s.Type == "Sell EMA"));

                // chercher signaux RSI pour la date
                var rsiSignal = signals
                    .FirstOrDefault(s =>
                        s.Date.Date == price.Date.Date &&
                        (s.Type == "Buy RSI" || s.Type == "Sell RSI"));

                // ajouter ligne resultat
                results.Add(new AnalysisResultViewModel(
                    price.Date,
                    price.ClosePrice,
                    sma?.Value,
                    ema?.Value,
                    rsi?.Value,
                    smaSignal?.Type,
                    emaSignal?.Type,
                    rsiSignal?.Type,
                    stock.Ticker,
                    stock.Name,
                    combinedSignal
                ));
            }

            return View(results);
        }
    }
}