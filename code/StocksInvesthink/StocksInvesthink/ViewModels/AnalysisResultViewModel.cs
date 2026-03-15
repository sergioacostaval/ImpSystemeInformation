namespace StocksInvesthink.ViewModels
{
    // objet utilise pour afficher les resultats dans la vue
    public class AnalysisResultViewModel
    {
        public DateTime Date { get; private set; }
        public decimal ClosePrice { get; private set; }
        public decimal? Sma { get; private set; }
        public decimal? Ema { get; private set; }
        public decimal? Rsi { get; private set; }

        // constructeur pour initialiser les donnees
        public AnalysisResultViewModel(
            DateTime date,
            decimal closePrice,
            decimal? sma,
            decimal? ema,
            decimal? rsi)
        {
            Date = date;
            ClosePrice = closePrice;
            Sma = sma;
            Ema = ema;
            Rsi = rsi;
        }
    }
}