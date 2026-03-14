namespace StocksInvesthink.Models
{
    public class HistoricalPrice
    {
        public int HistoricalPriceId { get; private set; }

        public DateTime Date { get; private set; }
        public decimal OpenPrice { get; private set; }
        public decimal HighPrice { get; private set; }
        public decimal LowPrice { get; private set; }
        public decimal ClosePrice { get; private set; }
        public long Volume { get; private set; }

        // Clé composite vers UserStock
        public int UserId { get; private set; }
        public int StockId { get; private set; }

        public UserStock UserStock { get; private set; } = null!;
        public ICollection<IndicatorValue> IndicatorValues { get; private set; } = new List<IndicatorValue>();

        // Constructeur vide requis par EF Core
        private HistoricalPrice()
        {
        }

        // Constructeur principal pour créer un prix historique valide
        public HistoricalPrice(
            DateTime date,
            decimal openPrice,
            decimal highPrice,
            decimal lowPrice,
            decimal closePrice,
            long volume,
            int userId,
            int stockId)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId invalide.");

            if (stockId <= 0)
                throw new ArgumentException("StockId invalide.");

            if (volume < 0)
                throw new ArgumentException("Le volume ne peut pas être négatif.");

            Date = date;
            OpenPrice = openPrice;
            HighPrice = highPrice;
            LowPrice = lowPrice;
            ClosePrice = closePrice;
            Volume = volume;
            UserId = userId;
            StockId = stockId;
        }
    }
}