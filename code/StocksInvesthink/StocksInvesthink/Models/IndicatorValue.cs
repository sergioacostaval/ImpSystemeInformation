namespace StocksInvesthink.Models
{
    public class IndicatorValue
    {
        public int IndicatorValueId { get; set; }

        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public int IndicatorInstanceId { get; set; }
        public IndicatorInstance IndicatorInstance { get; set; }

        public int HistoricalPriceId { get; set; }
        public HistoricalPrice HistoricalPrice { get; set; }

        public ICollection<Signal> Signals { get; set; } = new List<Signal>();
    }
}
