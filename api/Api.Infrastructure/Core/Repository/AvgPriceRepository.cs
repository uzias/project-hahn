using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using Api.Domain.WorkerService.Models;
using Api.Infrastructure.Core.Contracts;

namespace Api.Infrastructure.Core.Repository
{
    public class AvgPriceRepository : IAvgPriceRepository
    {
        private IHttpClientFactory _httpClientFactory;

        public AvgPriceRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<AvgPrice> GetAvgPriceAsync(String symbol)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(CreateRequestMessage(symbol));
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AvgPrice>(content) ?? throw new Exception("Failed to deserialize data from Binance API");
            }
            throw new Exception("Failed to fetch data from Binance API");
        }

        private HttpRequestMessage CreateRequestMessage(String symbol)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"https://api.binance.com/api/v3/avgPrice?symbol={symbol}");
        }
    }
}