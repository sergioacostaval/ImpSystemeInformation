namespace StocksInvesthink.Models
{
    public class Signal
    {
        public int SignalId { get; set; }

        public string Type { get; set; } = string.Empty; // Buy / Sell

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public int IndicatorValueId { get; set; }
        public IndicatorValue IndicatorValue { get; set; }
    }
}
