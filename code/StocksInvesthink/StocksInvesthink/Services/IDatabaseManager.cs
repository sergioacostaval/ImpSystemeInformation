using Microsoft.Data.Sqlite;

namespace StocksInvesthink.Services
{
    public interface IDatabaseManager
    {
        SqliteConnection GetConnection();
    }
}

