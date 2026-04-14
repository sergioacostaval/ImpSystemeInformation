using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Models;

namespace StocksInvesthink.Services
{
    // Service responsable de générer des signaux
    public class SignalService
    {
        private readonly StocksInvesthinkContext _db;

        public SignalService(StocksInvesthinkContext db)
        {
            _db = db;
        }

        // Générer les signaux Buy / Sell selon Prix V.S. SMA
        public async Task<int> GenerateSignalsFromSmaAsync(int userId, int stockId, int period = 20)
        {
            // Récupérer le type SMA
            var smaType = await _db.IndicatorTypes
                .FirstOrDefaultAsync(t => t.Name == "SMA");

            if (smaType == null)
                return 0;

            // Récupérer l'instance SMA
            var smaInstance = await _db.IndicatorInstances
                .Where(i => i.UserId == userId &&
                            i.StockId == stockId &&
                            i.IndicatorTypeId == smaType.IndicatorTypeId &&
                            i.Period == period)
                .OrderByDescending(i => i.IndicatorInstanceId)
                .FirstOrDefaultAsync();

            if (smaInstance == null)
                return 0;

            // Récupérer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            // Récupérer les valeurs SMA
            var smaValues = await _db.IndicatorValues
                .Where(v => v.IndicatorInstanceId == smaInstance.IndicatorInstanceId)
                .OrderBy(v => v.Date)
                .ToListAsync();

            if (prices.Count < 2 || smaValues.Count < 2)
                return 0;

            var signals = new List<Signal>();

            // Créer un dictionnaire Date: SMA pour accès rapide
            var smaByDate = smaValues
                .GroupBy(v => v.Date.Date)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(x => x.IndicatorValueId).First());

            for (int i = 1; i < prices.Count; i++)
            {
                var previousPrice = prices[i - 1];
                var currentPrice = prices[i];

                if (!smaByDate.ContainsKey(previousPrice.Date.Date) ||
                    !smaByDate.ContainsKey(currentPrice.Date.Date))
                    continue;

                var previousSma = smaByDate[previousPrice.Date.Date];
                var currentSma = smaByDate[currentPrice.Date.Date];

                // Signal Buy
                if (previousPrice.ClosePrice <= previousSma.Value &&
                    currentPrice.ClosePrice > currentSma.Value)
                {
                    signals.Add(new Signal(
                        "Buy",
                        currentPrice.Date,
                        currentPrice.ClosePrice,
                        currentSma.IndicatorValueId
                    ));
                }

                // Signal Sell
                if (previousPrice.ClosePrice >= previousSma.Value &&
                    currentPrice.ClosePrice < currentSma.Value)
                {
                    signals.Add(new Signal(
                        "Sell",
                        currentPrice.Date,
                        currentPrice.ClosePrice,
                        currentSma.IndicatorValueId
                    ));
                }
            }

            _db.Signals.AddRange(signals);
            await _db.SaveChangesAsync();

            return signals.Count;
        }

        // Générer les signaux à partir de l'EMA
        public async Task<int> GenerateSignalsFromEmaAsync(int userId, int stockId, int period = 20)
        {
            // Récupérer le type EMA
            var emaType = await _db.IndicatorTypes
                .FirstOrDefaultAsync(t => t.Name == "EMA");

            if (emaType == null)
                return 0;

            // Récupérer l'instance EMA
            var emaInstance = await _db.IndicatorInstances
                .Where(i => i.UserId == userId &&
                            i.StockId == stockId &&
                            i.IndicatorTypeId == emaType.IndicatorTypeId &&
                            i.Period == period)
                .OrderByDescending(i => i.IndicatorInstanceId)
                .FirstOrDefaultAsync();

            if (emaInstance == null)
                return 0;

            // Récupérer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            // Récupérer les valeurs EMA
            var emaValues = await _db.IndicatorValues
                .Where(v => v.IndicatorInstanceId == emaInstance.IndicatorInstanceId)
                .OrderBy(v => v.Date)
                .ToListAsync();

            if (prices.Count < 2 || emaValues.Count < 2)
                return 0;

            var signals = new List<Signal>();

            // Créer un dictionnaire Date : EMA pour accès rapide
            var emaByDate = emaValues
                .GroupBy(v => v.Date.Date)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(x => x.IndicatorValueId).First());

            for (int i = 1; i < prices.Count; i++)
            {
                var prevPrice = prices[i - 1];
                var currPrice = prices[i];

                if (!emaByDate.ContainsKey(prevPrice.Date.Date) ||
                    !emaByDate.ContainsKey(currPrice.Date.Date))
                    continue;

                var prevEma = emaByDate[prevPrice.Date.Date];
                var currEma = emaByDate[currPrice.Date.Date];

                // Signal Buy
                if (prevPrice.ClosePrice <= prevEma.Value &&
                    currPrice.ClosePrice > currEma.Value)
                {
                    signals.Add(new Signal(
                        "Buy EMA",
                        currPrice.Date,
                        currPrice.ClosePrice,
                        currEma.IndicatorValueId
                    ));
                }

                // Signal Sell
                if (prevPrice.ClosePrice >= prevEma.Value &&
                    currPrice.ClosePrice < currEma.Value)
                {
                    signals.Add(new Signal(
                        "Sell EMA",
                        currPrice.Date,
                        currPrice.ClosePrice,
                        currEma.IndicatorValueId
                    ));
                }
            }

            _db.Signals.AddRange(signals);
            await _db.SaveChangesAsync();

            return signals.Count;
        }

        // Générer les signaux à partir du RSI
        public async Task<int> GenerateSignalsFromRsiAsync(int userId, int stockId, int period = 14)
        {
            var rsiType = await _db.IndicatorTypes
                .FirstOrDefaultAsync(t => t.Name == "RSI");

            if (rsiType == null)
                return 0;

            var rsiInstance = await _db.IndicatorInstances
                .Where(i => i.UserId == userId &&
                            i.StockId == stockId &&
                            i.IndicatorTypeId == rsiType.IndicatorTypeId &&
                            i.Period == period)
                .OrderByDescending(i => i.IndicatorInstanceId)
                .FirstOrDefaultAsync();

            if (rsiInstance == null)
                return 0;

            var rsiValues = await _db.IndicatorValues
                .Include(v => v.HistoricalPrice)
                .Where(v => v.IndicatorInstanceId == rsiInstance.IndicatorInstanceId)
                .OrderBy(v => v.Date)
                .ToListAsync();

            if (rsiValues.Count < 2)
                return 0;

            var signals = new List<Signal>();

            for (int i = 1; i < rsiValues.Count; i++)
            {
                var previousRsi = rsiValues[i - 1];
                var currentRsi = rsiValues[i];

                // Buy RSI : le RSI remonte au-dessus de 30
                if (previousRsi.Value <= 30 && currentRsi.Value > 30)
                {
                    signals.Add(new Signal(
                        "Buy RSI",
                        currentRsi.Date,
                        currentRsi.HistoricalPrice.ClosePrice,
                        currentRsi.IndicatorValueId
                    ));
                }

                // Sell RSI : le RSI redescend sous 70
                if (previousRsi.Value >= 70 && currentRsi.Value < 70)
                {
                    signals.Add(new Signal(
                        "Sell RSI",
                        currentRsi.Date,
                        currentRsi.HistoricalPrice.ClosePrice,
                        currentRsi.IndicatorValueId
                    ));
                }
            }

            _db.Signals.AddRange(signals);
            await _db.SaveChangesAsync();

            return signals.Count;
        }
    }
}
