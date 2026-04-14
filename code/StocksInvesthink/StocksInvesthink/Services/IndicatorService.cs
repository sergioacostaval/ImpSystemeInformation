using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Models;

namespace StocksInvesthink.Services
{
    // Service responsable du calcul des indicateurs techniques
    public class IndicatorService
    {
        private readonly StocksInvesthinkContext _db;

        public IndicatorService(StocksInvesthinkContext db)
        {
            _db = db;
        }

        // Calcul du SMA
        public async Task<int> GenerateSmaAsync(int userId, int stockId, int period = 20)
        {
            // Récupérer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            if (prices.Count < period)
                return 0;

            // Récupérer le type d'indicateur SMA
            var smaType = await _db.IndicatorTypes
                .FirstOrDefaultAsync(i => i.Name == "SMA");

            if (smaType == null)
                return 0;

            // Créer l'instance de SMA
            var instance = new IndicatorInstance(period, smaType.IndicatorTypeId, userId, stockId);

            _db.IndicatorInstances.Add(instance);
            await _db.SaveChangesAsync();

            var values = new List<IndicatorValue>();

            // Calcul du SMA
            for (int i = period - 1; i < prices.Count; i++)
            {
                var window = prices.Skip(i - period + 1).Take(period);

                decimal sma = window.Average(p => p.ClosePrice);

                values.Add(new IndicatorValue(
                    prices[i].Date,
                    sma,
                    instance.IndicatorInstanceId,
                    prices[i].HistoricalPriceId
                ));
            }

            _db.IndicatorValues.AddRange(values);
            await _db.SaveChangesAsync();

            return values.Count;
        }

        // Calcul de l'indicateur EMA
        public async Task<int> GenerateEmaAsync(int userId, int stockId, int period = 20)
        {
            // Récupérer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            if (prices.Count < period)
                return 0;

            // Récupérer le type EMA
            var emaType = await _db.IndicatorTypes
                .FirstOrDefaultAsync(i => i.Name == "EMA");

            if (emaType == null)
                return 0;

            // Créer l'instance EMA
            var instance = new IndicatorInstance(period, emaType.IndicatorTypeId, userId, stockId);

            _db.IndicatorInstances.Add(instance);
            await _db.SaveChangesAsync();

            var values = new List<IndicatorValue>();

            // Constante EMA
            decimal k = 2m / (period + 1);

            // Première EMA = SMA
            decimal ema = prices.Take(period).Average(p => p.ClosePrice);

            values.Add(new IndicatorValue(
                prices[period - 1].Date,
                ema,
                instance.IndicatorInstanceId,
                prices[period - 1].HistoricalPriceId
            ));

            // Calcul EMA récursif
            for (int i = period; i < prices.Count; i++)
            {
                ema = (prices[i].ClosePrice * k) + (ema * (1 - k));

                values.Add(new IndicatorValue(
                    prices[i].Date,
                    ema,
                    instance.IndicatorInstanceId,
                    prices[i].HistoricalPriceId
                ));
            }

            _db.IndicatorValues.AddRange(values);
            await _db.SaveChangesAsync();

            return values.Count;
        }

        // Calcul de l'indicateur RSI
        public async Task<int> GenerateRsiAsync(int userId, int stockId, int period = 14)
        {
            // Récupérer les prix historiques
            var prices = await _db.HistoricalPrices
                .Where(p => p.UserId == userId && p.StockId == stockId)
                .OrderBy(p => p.Date)
                .ToListAsync();

            if (prices.Count <= period)
                return 0;

            // Récupérer le type RSI
            var rsiType = await _db.IndicatorTypes
                .FirstOrDefaultAsync(i => i.Name == "RSI");

            if (rsiType == null)
                return 0;

            // Créer l'instance de RSI
            var instance = new IndicatorInstance(period, rsiType.IndicatorTypeId, userId, stockId);

            _db.IndicatorInstances.Add(instance);
            await _db.SaveChangesAsync();

            var values = new List<IndicatorValue>();

            decimal gainSum = 0;
            decimal lossSum = 0;

            // Calcul initial des gains et pertes
            for (int i = 1; i <= period; i++)
            {
                decimal change = prices[i].ClosePrice - prices[i - 1].ClosePrice;

                if (change > 0)
                    gainSum += change;
                else
                    lossSum += Math.Abs(change);
            }

            decimal avgGain = gainSum / period;
            decimal avgLoss = lossSum / period;

            decimal rs = avgLoss == 0 ? 0 : avgGain / avgLoss;
            decimal rsi = avgLoss == 0 ? 100 : 100 - (100 / (1 + rs));

            values.Add(new IndicatorValue(
                prices[period].Date,
                rsi,
                instance.IndicatorInstanceId,
                prices[period].HistoricalPriceId
            ));

            // Calcul progressif du RSI
            for (int i = period + 1; i < prices.Count; i++)
            {
                decimal change = prices[i].ClosePrice - prices[i - 1].ClosePrice;

                decimal gain = change > 0 ? change : 0;
                decimal loss = change < 0 ? Math.Abs(change) : 0;

                avgGain = ((avgGain * (period - 1)) + gain) / period;
                avgLoss = ((avgLoss * (period - 1)) + loss) / period;

                rs = avgLoss == 0 ? 0 : avgGain / avgLoss;
                rsi = avgLoss == 0 ? 100 : 100 - (100 / (1 + rs));

                values.Add(new IndicatorValue(
                    prices[i].Date,
                    rsi,
                    instance.IndicatorInstanceId,
                    prices[i].HistoricalPriceId
                ));
            }

            _db.IndicatorValues.AddRange(values);
            await _db.SaveChangesAsync();

            return values.Count;
        }
    }
}
