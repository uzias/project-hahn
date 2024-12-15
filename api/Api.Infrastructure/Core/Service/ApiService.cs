using Api.Domain.WorkerService.Models;
using Api.Infrastructure.Core.Contracts;

namespace Api.Infrastructure.Core.Service
{
    public class ApiService : IApiService
    {
        private IAvgPriceRepository _apiRepository;

        public ApiService(IAvgPriceRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public Task<AvgPrice> GetAvgPriceAsync(String symbol)
        {
            return _apiRepository.GetAvgPriceAsync(symbol);
        }
    }
}