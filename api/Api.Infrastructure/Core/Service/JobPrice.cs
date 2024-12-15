using Api.Domain.Common.Model;
using Api.Infrastructure.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace Api.Infrastructure.Core.Service
{
    
    public class JobPrice : IJobPrice
    {
        protected IApiService _apiService;
        protected IPairPriceRepository _priceRepository;
        protected ILogger<JobPrice> _logger;
        public JobPrice(IPairPriceRepository priceRepository, IApiService apiService, ILogger<JobPrice> logger)
        {
            _priceRepository = priceRepository;
            _apiService = apiService;
            _logger = logger;

        }
        
        public async Task Execute(String symbol)
        {
           _logger.LogTrace($"Getting price for symbol: {symbol}");
           var avgPrice = await _apiService.GetAvgPriceAsync(symbol);
           _logger.LogDebug($"Symbol: {symbol} - Price: {avgPrice.Price} - Mins: {avgPrice.Mins}");
           var pairPrice = new PairPrice()
            {
                Symbol = symbol,
                Price = avgPrice.Price,
                Mins = avgPrice.Mins
            };
            await _priceRepository.UpsertAsync(pairPrice);
        }

    }
}