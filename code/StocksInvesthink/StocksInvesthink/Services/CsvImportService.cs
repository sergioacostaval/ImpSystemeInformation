using System.Globalization;
using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Models;

namespace StocksInvesthink.Services
{
    // Service responsable de l'importation des fichiers CSV
    public class CsvImportService
    {
        private readonly StocksInvesthinkContext _db;

        // Inclusion de DbContext pour accéder à la base de données
        public CsvImportService(StocksInvesthinkContext db)
        {
            _db = db;
        }

        // Méthode pour importer les données historiques d'un CSV
        public async Task<int> ImportYahooAsync(IFormFile file, int userId, int stockId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("CSV vide");

            bool userExists = await _db.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
                throw new ArgumentException("Utilisateur invalide.");

            bool stockExists = await _db.Stocks.AnyAsync(s => s.StockId == stockId);
            if (!stockExists)
                throw new ArgumentException("Stock invalide.");

            var userStock = await _db.UserStocks
                .FirstOrDefaultAsync(us => us.UserId == userId && us.StockId == stockId);

            if (userStock == null)
            {
                userStock = new UserStock(userId, stockId);

                _db.UserStocks.Add(userStock);
                await _db.SaveChangesAsync();
            }

            var prices = new List<HistoricalPrice>();

            // Lecture du fichier CSV
            using var reader = new StreamReader(file.OpenReadStream());

            int lineNumber = 0;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lineNumber++;

                // Ignorer les lignes vides
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (lineNumber <= 3)
                    continue;

                var parts = line.Split(',');

                // Vérifier que la ligne contient toutes les colonnes nécessaires
                if (parts.Length < 6)
                    continue;

                // Format attendu : Date,Close,High,Low,Open,Volume
                if (!DateTime.TryParse(parts[0], out var date))
                    continue;

                // Conversion des valeurs numériques
                decimal open = decimal.Parse(parts[4], CultureInfo.InvariantCulture);
                decimal high = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
                decimal low = decimal.Parse(parts[3], CultureInfo.InvariantCulture);
                decimal close = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                long volume = long.Parse(parts[5]);

                prices.Add(new HistoricalPrice(
                    date,
                    open,
                    high,
                    low,
                    close,
                    volume,
                    userId,
                    stockId));
            }

            _db.HistoricalPrices.AddRange(prices);
            await _db.SaveChangesAsync();
            return prices.Count;
        }
    }
}