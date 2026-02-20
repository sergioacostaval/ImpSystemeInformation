namespace StocksInvesthink.Models
{
    public class IndicatorType
    {
        public int IndicatorTypeId { get; set; }

        public string Name { get; set; } = string.Empty; // SMA, EMA, RSI

        public ICollection<IndicatorInstance> IndicatorInstances { get; set; } = new List<IndicatorInstance>();
    }
}
