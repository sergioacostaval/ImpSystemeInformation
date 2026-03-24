using StocksInvesthink.Services.Commands.Interfaces;

namespace StocksInvesthink.Services.Commands
{
    // Commande qui importe les nouveaux prix historiques depuis le CSV
    public class ImportHistoricalPricesCommand : IAnalysisCommand
    {
        private readonly CsvImportService _csvImportService;
        private readonly IFormFile _file;
        private readonly int _userId;
        private readonly int _stockId;

        public int ImportedRows { get; private set; }

        public ImportHistoricalPricesCommand(CsvImportService csvImportService, IFormFile file, int userId, int stockId)
        {
            _csvImportService = csvImportService;
            _file = file;
            _userId = userId;
            _stockId = stockId;
        }

        public async Task ExecuteAsync()
        {
            // Importer le nouveau fichier CSV
            ImportedRows = await _csvImportService.ImportYahooAsync(_file, _userId, _stockId);
        }
    }
}
