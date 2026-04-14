using StocksInvesthink.Data;
using StocksInvesthink.Services;
using StocksInvesthink.Services.Commands;

namespace StocksInvesthink.Services.Facade
{
    // Façade qui simplifie le processus d'analyse
    public class StockAnalysisFacade : IStockAnalysisFacade
    {
        private readonly StocksInvesthinkContext _db;
        private readonly CsvImportService _csvImportService;
        private readonly IndicatorService _indicatorService;
        private readonly SignalService _signalService;

        // Injection des services nécessaires à la façade
        public StockAnalysisFacade(
            StocksInvesthinkContext db,
            CsvImportService csvImportService,
            IndicatorService indicatorService,
            SignalService signalService)
        {
            _db = db;
            _csvImportService = csvImportService;
            _indicatorService = indicatorService;
            _signalService = signalService;
        }

        // Lance le processus d'analyse (Herite de L'Interface)
        public async Task<int> RunFullAnalysisAsync(IFormFile file, int userId, int stockId)
        {
            // Créer les commandes du processus
            var clearCommand = new ClearAnalysisDataCommand(_db, userId, stockId);
            var importCommand = new ImportHistoricalPricesCommand(_csvImportService, file, userId, stockId);
            var calculateCommand = new RunIndicatorsAndSignalsCommand(_indicatorService, _signalService, userId, stockId);

            // Préparer l'invoker
            var invoker = new AnalysisCommandInvoker();
            invoker.AddCommand(clearCommand);
            invoker.AddCommand(importCommand);
            invoker.AddCommand(calculateCommand);

            // Utiliser une transaction pour remplacer complètement l'analyse (Ne sauvegarde dans la Base de données jusqu' qu'il soit completé)
            await using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                await invoker.ExecuteAllAsync();
                await transaction.CommitAsync(); //si tout est correct, il sauvegarde dans la BD

                return importCommand.ImportedRows;
            }
            //Si qqch ne fonctionne pas (erreur) on delete les changements et on se sauvegarde pas dans la BD
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}