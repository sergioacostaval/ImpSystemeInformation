using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Services.Commands.Interfaces;

namespace StocksInvesthink.Services.Commands
{
    // Commande qui nettoie les anciennes données d'analyse d'un stock
    public class ClearAnalysisDataCommand : IAnalysisCommand
    {
        private readonly StocksInvesthinkContext _db;
        private readonly int _userId;
        private readonly int _stockId;

        public ClearAnalysisDataCommand(StocksInvesthinkContext db, int userId, int stockId)
        {
            _db = db;
            _userId = userId;
            _stockId = stockId;
        }

        public async Task ExecuteAsync()
        {
            // Récupérer les anciennes instances d'indicateurs du user et du stock
            var indicatorInstances = await _db.IndicatorInstances
                .Where(i => i.UserId == _userId && i.StockId == _stockId)
                .ToListAsync();

            if (indicatorInstances.Count > 0)
            {
                var instanceIds = indicatorInstances
                    .Select(i => i.IndicatorInstanceId)
                    .ToList();

                // Récupérer les anciennes valeurs d'indicateurs liées aux instances
                var indicatorValues = await _db.IndicatorValues
                    .Where(v => instanceIds.Contains(v.IndicatorInstanceId))
                    .ToListAsync();

                if (indicatorValues.Count > 0)
                {
                    var valueIds = indicatorValues
                        .Select(v => v.IndicatorValueId)
                        .ToList();

                    // Supprimer les anciens signaux liés aux valeurs d'indicateurs
                    var signals = await _db.Signals
                        .Where(s => valueIds.Contains(s.IndicatorValueId))
                        .ToListAsync();

                    if (signals.Count > 0)
                    {
                        _db.Signals.RemoveRange(signals);
                    }

                    // Supprimer les anciennes valeurs d'indicateurs
                    _db.IndicatorValues.RemoveRange(indicatorValues);
                }

                // Supprimer les anciennes instances d'indicateurs
                _db.IndicatorInstances.RemoveRange(indicatorInstances);
            }

            // Récupérer et supprimer les anciens prix historiques
            var historicalPrices = await _db.HistoricalPrices
                .Where(p => p.UserId == _userId && p.StockId == _stockId)
                .ToListAsync();

            if (historicalPrices.Count > 0)
            {
                _db.HistoricalPrices.RemoveRange(historicalPrices);
            }

            await _db.SaveChangesAsync();
        }
    }
}
