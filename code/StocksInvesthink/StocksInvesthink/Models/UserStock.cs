namespace StocksInvesthink.Models
{
    public class UserStock
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public int StockId { get; private set; }
        public Stock Stock { get; private set; } = null!;

        public ICollection<HistoricalPrice> HistoricalPrices { get; private set; } = new List<HistoricalPrice>();
        public ICollection<IndicatorInstance> IndicatorInstances { get; private set; } = new List<IndicatorInstance>();

        // Constructeur vide requis par EF Core
        private UserStock()
        {
        }

        // Constructeur principal
        public UserStock(int userId, int stockId)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId invalide.");

            if (stockId <= 0)
                throw new ArgumentException("StockId invalide.");

            UserId = userId;
            StockId = stockId;
        }
    }
}