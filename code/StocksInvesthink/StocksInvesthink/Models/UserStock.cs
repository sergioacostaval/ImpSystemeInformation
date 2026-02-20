namespace StocksInvesthink.Models
{
    public class UserStock
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public ICollection<HistoricalPrice> HistoricalPrices { get; set; } = new List<HistoricalPrice>();

        public ICollection<IndicatorInstance> IndicatorInstances { get; set; } = new List<IndicatorInstance>();
    }
}
