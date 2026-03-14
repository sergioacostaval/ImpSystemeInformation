using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StocksInvesthink.Data;
using StocksInvesthink.Services.Facade;

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
    }
}