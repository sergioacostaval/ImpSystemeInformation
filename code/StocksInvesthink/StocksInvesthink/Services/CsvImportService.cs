using System.Globalization;
using StocksInvesthink.Data;
using StocksInvesthink.Models;

namespace StocksInvesthink.Services
{
    // Service responsable de l'importation des fichiers CSV provenant de Yahoo Finance
    public class CsvImportService
    {
        private readonly StocksInvesthinkContext _db;

        // Injection du DbContext pour accéder à la base de données
        public CsvImportService(StocksInvesthinkContext db)
        {
            _db = db;
        }

        // Méthode principale pour importer les données historiques d'un CSV Yahoo Finance
        public async Task<int> ImportYahooAsync(IFormFile file, int userId, int stockId)
        {
            // Vérifier si le fichier est vide
            if (file == null || file.Length == 0)
                throw new ArgumentException("CSV vide");

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

                // Les fichiers Yahoo Finance contiennent 3 lignes d'en-tête à ignorer
                if (lineNumber <= 3)
                    continue;

                var parts = line.Split(',');

                // Vérifier que la ligne contient toutes les colonnes nécessaires
                if (parts.Length < 6)
                    continue;

                // Format attendu :
                // Date,Close,High,Low,Open,Volume

                if (!DateTime.TryParse(parts[0], out var date))
                    continue;

                // Conversion des valeurs numériques avec CultureInfo.InvariantCulture
                decimal open = decimal.Parse(parts[4], CultureInfo.InvariantCulture);
                decimal high = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
                decimal low = decimal.Parse(parts[3], CultureInfo.InvariantCulture);
                decimal close = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                long volume = long.Parse(parts[5]);

                // Création d'un nouvel objet HistoricalPrice
                prices.Add(new HistoricalPrice
                {
                    Date = date,
                    OpenPrice = open,
                    HighPrice = high,
                    LowPrice = low,
                    ClosePrice = close,
                    Volume = volume,

                    // Clés étrangères
                    UserId = 1,
                    StockId = 1
                });
            }

            // Ajout de toutes les lignes dans la base de données
            _db.HistoricalPrices.AddRange(prices);

            // Sauvegarde des modifications dans la base
            await _db.SaveChangesAsync();

            // Retourne le nombre de lignes importées
            return prices.Count;
        }
    }
}