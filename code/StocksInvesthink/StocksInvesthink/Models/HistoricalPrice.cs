namespace StocksInvesthink.Models
{
    public class HistoricalPrice
    {
        public int HistoricalPriceId { get; set; }

        public DateTime Date { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public decimal ClosePrice { get; set; }

        public long Volume { get; set; }

        public int UserId { get; set; }
        public int StockId { get; set; }

        public UserStock UserStock { get; set; }

        public ICollection<IndicatorValue> IndicatorValues { get; set; } = new List<IndicatorValue>();
    }
}
