using StocksInvesthink.Services;

namespace StocksInvesthink.Services.Facade
{
    // Façade qui simplifie le processus d'analyse
    public class StockAnalysisFacade : IStockAnalysisFacade
    {
        private readonly CsvImportService _csvImportService;
        private readonly IndicatorService _indicatorService;
        private readonly SignalService _signalService;

        //Injection des services nécessaires à la façade
        public StockAnalysisFacade(
            CsvImportService csvImportService,
            IndicatorService indicatorService,
            SignalService signalService)
        {
            _csvImportService = csvImportService;
            _indicatorService = indicatorService;
            _signalService = signalService;
        }

        // Lance tout le processus d'analyse
        public async Task<int> RunFullAnalysisAsync(IFormFile file, int userId, int stockId)
        {
            // importer le fichier CSV
            int importedRows = await _csvImportService.ImportYahooAsync(file, userId, stockId);
            // calcul d'indicateurs
            await _indicatorService.GenerateSmaAsync(userId, stockId); //SMA
            await _indicatorService.GenerateEmaAsync(userId, stockId); //EMA
            await _indicatorService.GenerateRsiAsync(userId, stockId); //RSI

            // génération de signaux
            await _signalService.GenerateSignalsFromSmaAsync(userId, stockId); //SMA
            await _signalService.GenerateSignalsFromEmaAsync(userId, stockId); //EMA
            await _signalService.GenerateSignalsFromRsiAsync(userId, stockId); //RSI

            return importedRows;
        }
    }
}