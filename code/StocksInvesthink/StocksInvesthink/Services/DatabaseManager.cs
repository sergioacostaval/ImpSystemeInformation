using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;

namespace StocksInvesthink.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly DbContextOptions<StocksInvesthinkContext> _options;

        public DatabaseManager()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StocksInvesthinkContext>();
            optionsBuilder.UseSqlite("Data Source=stocksinvesthink.db");

            _options = optionsBuilder.Options;
        }

        public DbContextOptions<StocksInvesthinkContext> GetOptions()
        {
            return _options;
        }
    }
}
