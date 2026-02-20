namespace StocksInvesthink.Models
{
    public class IndicatorInstance
    {
        public int Id { get; set; }

        public int Period { get; set; }

        public int IndicatorTypeId { get; set; }
        public IndicatorType IndicatorType { get; set; }

        public int UserId { get; set; }
        public int StockId { get; set; }

        public UserStock UserStock { get; set; }

        public ICollection<IndicatorValue> IndicatorValues { get; set; }
    }
}
