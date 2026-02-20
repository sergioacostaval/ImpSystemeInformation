namespace StocksInvesthink.Models
{
    public class UserPreference
    {
        public int UserPreferenceId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int DefaultIndicatorTypeId { get; set; }
        public IndicatorType DefaultIndicatorType { get; set; }
        public int DefaultPeriod { get; set; }
    }
}
