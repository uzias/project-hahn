using Api.Domain.WorkerService.Models;

namespace Api.Infrastructure.Core.Contracts
{
    public interface IAvgPriceRepository
    {
        public Task<AvgPrice> GetAvgPriceAsync(String symbol);   
    }
}