using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Services
{
    public class GoldPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoldPriceService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<Decimal> GetGramPricesAsync()
        {
            string url = "https://www.goldapi.io/api/XAU/USD";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-access-token", _apiKey);

            request.Content = new StringContent("");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            JObject data = JObject.Parse(json);
            decimal price;
            if (decimal.TryParse(data["price_gram_24k"]?.ToString(), out price))
                return price;
            else
                throw new Exception("gold price is null");
        }
    }
}
