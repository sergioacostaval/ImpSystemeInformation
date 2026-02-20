namespace StocksInvesthink.Models;

public class Stock
{
    public int StockId { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<UserStock> UserStocks { get; set; } = new List<UserStock>();
}
