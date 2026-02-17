using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace StocksInvesthink.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}
