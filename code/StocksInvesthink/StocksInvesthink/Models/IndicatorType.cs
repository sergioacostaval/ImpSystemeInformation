namespace StocksInvesthink.Models
{
    public class IndicatorType
    {
        public int Id { get; set; }

        public string Name { get; set; } // SMA, EMA, RSI

        public ICollection<IndicatorInstance> IndicatorInstances { get; set; }
    }
}
