namespace StocksInvesthink.Models;

public class Stock
{
    public int StockId { get; private set; }
    public string Ticker { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public ICollection<UserStock> UserStocks { get; private set; } = new List<UserStock>();

    // Constructeur vide requis par EF Core
    private Stock()
    {
    }

    // Constructeur principal
    public Stock(string ticker, string name)
    {
        SetTicker(ticker);
        SetName(name);
    }

    // Modifier le ticker
    public void SetTicker(string ticker)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            throw new ArgumentException("Le ticker est obligatoire.");

        Ticker = ticker.Trim().ToUpper();
    }

    // Modifier le nom du stock
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Le nom du stock est obligatoire.");

        Name = name.Trim();
    }
}