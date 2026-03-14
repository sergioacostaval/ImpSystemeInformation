namespace StocksInvesthink.Services.Facade
{
    public interface IStockAnalysisFacade
    {
        Task<int> RunFullAnalysisAsync(IFormFile file, int userId, int stockId);
    }
}