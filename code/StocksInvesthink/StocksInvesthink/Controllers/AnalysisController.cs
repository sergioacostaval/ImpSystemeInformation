using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Services;

namespace StocksInvesthink.Controllers
{
    // Controller responsable des fonctionnalités d'analyse des stocks
    public class AnalysisController : Controller
    {
        private readonly StocksInvesthinkContext _db;
        private readonly CsvImportService _csv;

        // Injection du DbContext et du service d'import CSV
        public AnalysisController(StocksInvesthinkContext db, CsvImportService csv)
        {
            _db = db;
            _csv = csv;
        }

        // Affiche la page d'import CSV
        public async Task<IActionResult> Import()
        {
            // Charger les utilisateurs disponibles
            ViewBag.Users = await _db.Users.ToListAsync();

            // Charger les stocks disponibles
            ViewBag.Stocks = await _db.Stocks.ToListAsync();

            return View();
        }

        // Action appelée lorsque l'utilisateur soumet le formulaire d'import
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file, int userId, int stockId)
        {
            // Appeler le service d'import CSV
            var rows = await _csv.ImportYahooAsync(file, userId, stockId);

            // Message de confirmation affiché après l'import
            TempData["Message"] = $"Import terminé : {rows} lignes ajoutées";

            // Redirection vers la page d'import
            return RedirectToAction("Import");
        }
    }
}