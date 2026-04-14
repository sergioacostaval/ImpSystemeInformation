namespace StocksInvesthink.Models;

public class User
{
    public int UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    public ICollection<UserStock> UserStocks { get; private set; } = new List<UserStock>();
    public UserPreference? UserPreference { get; private set; }

    // Constructeur vide requis par EF Core
    private User()
    {
    }

    // Constructeur principal
    public User(string name, string email, string passwordHash)
    {
        SetName(name);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    // Modifier le nom de l'utilisateur
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Le nom est obligatoire.");

        Name = name.Trim();
    }

    // Modifier l'email de l'utilisateur
    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("L'email est obligatoire.");

        Email = email.Trim().ToLower();
    }

    // Modifier le mot de passe
    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Le mot de passe est obligatoire.");

        PasswordHash = passwordHash;
    }

    // Associer les préférences de l'utilisateur
    public void SetUserPreference(UserPreference preference)
    {
        UserPreference = preference ?? throw new ArgumentNullException(nameof(preference));
    }
}