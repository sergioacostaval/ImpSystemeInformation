namespace StocksInvesthink.Models
{
    public class IndicatorType
    {
        public int IndicatorTypeId { get; private set; }

        public string Name { get; private set; } = string.Empty; // SMA, EMA, RSI

        public ICollection<IndicatorInstance> IndicatorInstances { get; private set; } = new List<IndicatorInstance>();

        // Constructeur vide requis par EF Core
        private IndicatorType()
        {
        }

        // Constructeur principal
        public IndicatorType(string name)
        {
            SetName(name);
        }

        // Modifier le nom du type d'indicateur
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Le nom de l'indicateur est obligatoire.");

            Name = name.Trim().ToUpper();
        }
    }
}