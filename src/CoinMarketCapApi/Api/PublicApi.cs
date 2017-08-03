using CoinMarketCap.Helpers;
using CoinMarketCap.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMarketCap.Api
{
    public static class PublicApi
    {
        public static async Task<IList<TickerModel>> GetTickersAsync(string ticker = null, string currency = null, int limit = 0)
        {
            var url = $"https://api.coinmarketcap.com/v1/ticker/";
            var filters = new List<string>();

            if (!string.IsNullOrEmpty(currency))
            {
                filters.Add($"convert={currency}");
            }

            if (limit > 0)
            {
                filters.Add($"limit={limit}");
            }

            if (!string.IsNullOrEmpty(ticker))
            {
                url = $"{url}{ticker}/";
            }

            if (filters.Count > 0)
            {
                url = $"{url}?{string.Join("&", filters.ToArray())}";
            }

            string responseStr = await HttpUtilities.GetHttpResponseAsync(url);
            var result = JsonConvert.DeserializeObject<IEnumerable<TickerModel>>(responseStr);
            return result.ToList();
        }

        public static async Task<GlobalDataModel> GetGlobalDataAsync(string currency = null)
        {
            var url = $"https://api.coinmarketcap.com/v1/global/";
            var filters = new List<string>();

            if (!string.IsNullOrEmpty(currency))
            {
                url = $"{url}?convert={currency}";
            }

            string responseStr = await HttpUtilities.GetHttpResponseAsync(url);
            return JsonConvert.DeserializeObject<GlobalDataModel>(responseStr);
        }
    }
}
