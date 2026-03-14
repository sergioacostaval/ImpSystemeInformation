namespace StocksInvesthink.Models
{
    public class UserPreference
    {
        public int UserPreferenceId { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public int DefaultIndicatorTypeId { get; private set; }
        public IndicatorType DefaultIndicatorType { get; private set; } = null!;

        public int DefaultPeriod { get; private set; }

        // Constructeur vide requis par EF Core
        private UserPreference()
        {
        }

        // Constructeur principal
        public UserPreference(int userId, int defaultIndicatorTypeId, int defaultPeriod)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId invalide.");

            if (defaultIndicatorTypeId <= 0)
                throw new ArgumentException("DefaultIndicatorTypeId invalide.");

            if (defaultPeriod <= 0)
                throw new ArgumentException("La période par défaut doit être supérieure à 0.");

            UserId = userId;
            DefaultIndicatorTypeId = defaultIndicatorTypeId;
            DefaultPeriod = defaultPeriod;
        }

        // Modifier la période par défaut
        public void SetDefaultPeriod(int defaultPeriod)
        {
            if (defaultPeriod <= 0)
                throw new ArgumentException("La période par défaut doit être supérieure à 0.");

            DefaultPeriod = defaultPeriod;
        }
    }
}