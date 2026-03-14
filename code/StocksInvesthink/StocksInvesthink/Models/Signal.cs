namespace StocksInvesthink.Models
{
    public class Signal
    {
        public int SignalId { get; private set; }

        public string Type { get; private set; } = string.Empty; // Buy / Sell
        public DateTime Date { get; private set; }
        public decimal Price { get; private set; }

        public int IndicatorValueId { get; private set; }
        public IndicatorValue IndicatorValue { get; private set; } = null!;

        // Constructeur vide requis par EF Core
        private Signal()
        {
        }

        // Constructeur principal
        public Signal(string type, DateTime date, decimal price, int indicatorValueId)
        {
            SetType(type);

            if (indicatorValueId <= 0)
                throw new ArgumentException("IndicatorValueId invalide.");

            Date = date;
            Price = price;
            IndicatorValueId = indicatorValueId;
        }

        // Modifier le type de signal
        public void SetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Le type de signal est obligatoire.");

            Type = type.Trim();
        }
    }
}