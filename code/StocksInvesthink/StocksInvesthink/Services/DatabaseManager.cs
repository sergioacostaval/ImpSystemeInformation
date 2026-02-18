using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StocksInvesthink.Data;

namespace StocksInvesthink.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        private static DatabaseManager _instance;
        private readonly DbContextOptions<StocksInvesthinkContext> _options;

        private DatabaseManager()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StocksInvesthinkContext>();
            optionsBuilder.UseSqlite("Data Source=stocksinvesthink.db");

            _options = optionsBuilder.Options;
        }

        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseManager();
                }
                return _instance;
            }
        }

        public DbContextOptions<StocksInvesthinkContext> GetOptions()
        {
            return _options;
        }
    }
}
