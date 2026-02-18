namespace StocksInvesthink.Models;

public class Stock
{
    public int StockId { get; set; }
    public string Ticker { get; set; }
    public string Name { get; set; }
    public ICollection<UserStock> UserStocks { get; set; }
}
