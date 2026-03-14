namespace StocksInvesthink.Models
{
    public class IndicatorInstance
    {
        public int IndicatorInstanceId { get; private set; }

        public int Period { get; private set; }

        public int IndicatorTypeId { get; private set; }
        public IndicatorType IndicatorType { get; private set; } = null!;

        public int UserId { get; private set; }
        public int StockId { get; private set; }

        public UserStock UserStock { get; private set; } = null!;
        public ICollection<IndicatorValue> IndicatorValues { get; private set; } = new List<IndicatorValue>();

        // Constructeur vide requis par EF Core
        private IndicatorInstance()
        {
        }

        // Constructeur principal
        public IndicatorInstance(int period, int indicatorTypeId, int userId, int stockId)
        {
            SetPeriod(period);

            if (indicatorTypeId <= 0)
                throw new ArgumentException("IndicatorTypeId invalide.");

            if (userId <= 0)
                throw new ArgumentException("UserId invalide.");

            if (stockId <= 0)
                throw new ArgumentException("StockId invalide.");

            IndicatorTypeId = indicatorTypeId;
            UserId = userId;
            StockId = stockId;
        }

        // Modifier la période de calcul
        public void SetPeriod(int period)
        {
            if (period <= 0)
                throw new ArgumentException("La période doit être supérieure à 0.");

            Period = period;
        }
    }
}