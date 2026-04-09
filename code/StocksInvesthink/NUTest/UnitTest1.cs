using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Services;

namespace NUTest
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task Test_GenererSignaux()
        {
            var options = new DbContextOptionsBuilder<StocksInvesthinkContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new StocksInvesthinkContext(options);
            var service = new SignalService(context);

            int genereSignalSMA = await service.GenerateSignalsFromSmaAsync(1, 1, 20);
        }
    }
}
