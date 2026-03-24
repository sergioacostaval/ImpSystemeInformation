namespace StocksInvesthink.ViewModels
{
    // Utilise pour afficher les resultats dans la vue
    public class AnalysisResultViewModel
    {
        public DateTime Date { get; private set; }
        public decimal ClosePrice { get; private set; }
        public decimal? Sma { get; private set; }
        public decimal? Ema { get; private set; }
        public decimal? Rsi { get; private set; }
        public string? SmaSignal { get; private set; }
        public string? EmaSignal { get; private set; }
        public string? RsiSignal { get; private set; }
        public string StockTicker { get; private set; }
        public string StockName { get; private set; }

        // recommandation globale basee sur les signaux recents (10 derniers jours)
        public string? CombinedSignal { get; private set; }

        public AnalysisResultViewModel(
            DateTime date,
            decimal closePrice,
            decimal? sma,
            decimal? ema,
            decimal? rsi,
            string? smaSignal,
            string? emaSignal,
            string? rsiSignal,
            string stockTicker,
            string stockName,
            string? combinedSignal)
        {
            Date = date;
            ClosePrice = closePrice;
            Sma = sma;
            Ema = ema;
            Rsi = rsi;
            SmaSignal = smaSignal;
            EmaSignal = emaSignal;
            RsiSignal = rsiSignal;
            StockTicker = stockTicker;
            StockName = stockName;
            CombinedSignal = combinedSignal;
        }
    }
}