using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CoinMarketCap.Api;

namespace UnitTests
{
    [TestClass]
    public class PublicApiUnitTests
    {
        [TestMethod]
        public async Task TestGetTickersAsync()
        {
            var tickers = await PublicApi.GetTickersAsync();
            Assert.IsTrue(tickers.Count > 100, "Tickers count is zero");

            tickers = await PublicApi.GetTickersAsync(limit: 1);
            Assert.AreEqual(1, tickers.Count, "Tickers count is not 1");

            tickers = await PublicApi.GetTickersAsync(ticker: "bitcoin");
            Assert.AreEqual(1, tickers.Count, "Tickers count is not 1");
            Assert.AreEqual("bitcoin", tickers[0].Id, "Ticker id is not bitcoin");

            tickers = await PublicApi.GetTickersAsync(ticker: "bitcoin", currency: "INR");
            Assert.AreEqual(1, tickers.Count, "Tickers count is not 1");
            Assert.AreEqual("bitcoin", tickers[0].Id, "Ticker id is not bitcoin");
            Assert.IsNotNull(tickers[0].PriceINR, "PriceINR is null");
        }

        [TestMethod]
        public async Task TestGetGlobalDataAsync()
        {
            var globalData = await PublicApi.GetGlobalDataAsync();
            Assert.IsNotNull(globalData, "globalData is null");
            Assert.IsNotNull(globalData.TotalMarketCapUSD, "globalData.TotalMarketCapUSD is null");

            globalData = await PublicApi.GetGlobalDataAsync(currency: "INR");
            Assert.IsNotNull(globalData, "globalData is null");
            Assert.IsNotNull(globalData.TotalMarketCapUSD, "globalData.TotalMarketCapUSD is null");
            Assert.IsNotNull(globalData.TotalMarketCapINR, "globalData.TotalMarketCapINR is null");
        }
    }
}