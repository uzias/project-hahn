using Api.Domain.Common.Model;

namespace Api.Infrastructure.Core.Contracts
{
    public interface IPairPriceRepository
    {
        public  Task UpsertAsync(PairPrice pairPrice);

        public  Task<IEnumerable<PairPrice>> GetAllAsync();

        public  Task<PairPrice> GetBySymbolAsync(string symbol);
    }
}