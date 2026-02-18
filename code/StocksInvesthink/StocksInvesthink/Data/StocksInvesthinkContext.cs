using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Models;

namespace StocksInvesthink.Data
{
    public class StocksInvesthinkContext : DbContext
    {
        public StocksInvesthinkContext(DbContextOptions<StocksInvesthinkContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<HistoricalPrice> HistoricalPrices { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<Signal> Signals { get; set; }
    }
}
