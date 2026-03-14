namespace StocksInvesthink.Models
{
    public class IndicatorValue
    {
        public int IndicatorValueId { get; private set; }

        public DateTime Date { get; private set; }
        public decimal Value { get; private set; }

        public int IndicatorInstanceId { get; private set; }
        public IndicatorInstance IndicatorInstance { get; private set; } = null!;

        public int HistoricalPriceId { get; private set; }
        public HistoricalPrice HistoricalPrice { get; private set; } = null!;

        public ICollection<Signal> Signals { get; private set; } = new List<Signal>();

        // Constructeur vide requis par EF Core
        private IndicatorValue()
        {
        }

        // Constructeur principal
        public IndicatorValue(DateTime date, decimal value, int indicatorInstanceId, int historicalPriceId)
        {
            if (indicatorInstanceId <= 0)
                throw new ArgumentException("IndicatorInstanceId invalide.");

            if (historicalPriceId <= 0)
                throw new ArgumentException("HistoricalPriceId invalide.");

            Date = date;
            Value = value;
            IndicatorInstanceId = indicatorInstanceId;
            HistoricalPriceId = historicalPriceId;
        }
    }
}