using Api.Domain.WorkerService.Models;

namespace Api.Infrastructure.Core.Contracts
{
    public interface IApiService
    {
        public Task<AvgPrice> GetAvgPriceAsync(String symbol);   
    }
}