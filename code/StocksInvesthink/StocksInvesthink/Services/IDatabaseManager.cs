using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;

namespace StocksInvesthink.Services
{
    public interface IDatabaseManager
    {
        DbContextOptions<StocksInvesthinkContext> GetOptions();
    }
}

