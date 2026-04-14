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
        public DbSet<User> Users => Set<User>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<UserStock> UserStocks => Set<UserStock>();
        public DbSet<HistoricalPrice> HistoricalPrices => Set<HistoricalPrice>();
        public DbSet<IndicatorType> IndicatorTypes => Set<IndicatorType>();
        public DbSet<IndicatorInstance> IndicatorInstances => Set<IndicatorInstance>();
        public DbSet<IndicatorValue> IndicatorValues => Set<IndicatorValue>();
        public DbSet<Signal> Signals => Set<Signal>();
        public DbSet<UserPreference> UserPreferences => Set<UserPreference>();

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

            // Relation UserStock -> HistoricalPrices
            modelBuilder.Entity<UserStock>()
                .HasMany(us => us.HistoricalPrices)
                .WithOne(h => h.UserStock)
                .HasForeignKey(h => new { h.UserId, h.StockId })
                .OnDelete(DeleteBehavior.Cascade);

            // HistoricalPrices -> UserStock
            modelBuilder.Entity<HistoricalPrice>()
                .HasOne(h => h.UserStock)
                .WithMany(us => us.HistoricalPrices)
                .HasForeignKey(h => new { h.UserId, h.StockId });

            // IndicatorInstance -> UserStock
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

            // UserPreference -> User
            modelBuilder.Entity<UserPreference>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserPreference)
                .HasForeignKey<UserPreference>(up => up.UserId);

            // User Email Is Unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}