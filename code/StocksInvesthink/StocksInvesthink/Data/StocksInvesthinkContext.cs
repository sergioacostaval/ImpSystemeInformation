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

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
        public DbSet<HistoricalPrice> HistoricalPrices { get; set; }
        public DbSet<IndicatorType> IndicatorTypes { get; set; }
        public DbSet<IndicatorInstance> IndicatorInstances { get; set; }
        public DbSet<IndicatorValue> IndicatorValues { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for UserStock
            modelBuilder.Entity<UserStock>()
                .HasKey(us => new { us.UserId, us.StockId });

            modelBuilder.Entity<UserStock>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserStocks)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserStock>()
                .HasOne(us => us.Stock)
                .WithMany(s => s.UserStocks)
                .HasForeignKey(us => us.StockId);

            // HistoricalPrice relation with UserStock (composite FK)
            modelBuilder.Entity<HistoricalPrice>()
                .HasOne(h => h.UserStock)
                .WithMany(us => us.HistoricalPrices)
                .HasForeignKey(h => new { h.UserId, h.StockId });

            // IndicatorInstance relation with UserStock (composite FK)
            modelBuilder.Entity<IndicatorInstance>()
                .HasOne(ii => ii.UserStock)
                .WithMany(us => us.IndicatorInstances)
                .HasForeignKey(ii => new { ii.UserId, ii.StockId });

            // IndicatorInstance -> IndicatorType
            modelBuilder.Entity<IndicatorInstance>()
                .HasOne(ii => ii.IndicatorType)
                .WithMany(it => it.IndicatorInstances)
                .HasForeignKey(ii => ii.IndicatorTypeId);

            // IndicatorValue -> IndicatorInstance
            modelBuilder.Entity<IndicatorValue>()
                .HasOne(iv => iv.IndicatorInstance)
                .WithMany(ii => ii.IndicatorValues)
                .HasForeignKey(iv => iv.IndicatorInstanceId);

            // IndicatorValue -> HistoricalPrice
            modelBuilder.Entity<IndicatorValue>()
                .HasOne(iv => iv.HistoricalPrice)
                .WithMany(h => h.IndicatorValues)
                .HasForeignKey(iv => iv.HistoricalPriceId);

            // Signal -> IndicatorValue
            modelBuilder.Entity<Signal>()
                .HasOne(s => s.IndicatorValue)
                .WithMany(iv => iv.Signals)
                .HasForeignKey(s => s.IndicatorValueId);

            // UserPreference -> User (One-to-One)
            modelBuilder.Entity<UserPreference>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserPreference)
                .HasForeignKey<UserPreference>(up => up.UserId);
        }
    }
}
