namespace StocksInvesthink.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<UserStock> UserStocks { get; set; } = new List<UserStock>();
    public UserPreference UserPreference { get; set; }
}
