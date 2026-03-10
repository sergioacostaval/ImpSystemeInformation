using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StocksInvesthink.Data;
using StocksInvesthink.Services;

namespace StocksInvesthink.Controllers
{
    // L'utilisateur doit être authentifié pour accéder à l'analyse
    [Authorize]
    public class AnalysisController : Controller
    {
        private readonly StocksInvesthinkContext _db;
        private readonly CsvImportService _csv;

        // Injection du DbContext et du service CSV
        public AnalysisController(StocksInvesthinkContext db, CsvImportService csv)
        {
            _db = db;
            _csv = csv;
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

            // Import des données CSV pour cet utilisateur
            var rows = await _csv.ImportYahooAsync(file, userId, stockId);

            TempData["Message"] = $"Import terminé : {rows} lignes ajoutées";

            return RedirectToAction("Import");
        }
    }
}