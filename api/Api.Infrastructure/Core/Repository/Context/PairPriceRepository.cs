
using Api.Domain.Common.Model;
using Api.Infrastructure.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Core.Repository.Context
{
    public class PairPriceRepository : IPairPriceRepository
    {
        private readonly CryptoContext _context;
        public PairPriceRepository(CryptoContext context)
        {
            _context = context;
        }

        public async Task UpsertAsync(PairPrice pairPrice)
        {
            var existing = await _context.Price.FirstOrDefaultAsync(x => x.Symbol == pairPrice.Symbol);
            if (existing != null)
            {
                existing.Price = pairPrice.Price;
                existing.Mins = pairPrice.Mins;
                _context.Price.Update(existing);
            }
            else
            {
                await _context.Price.AddAsync(pairPrice);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PairPrice>> GetAllAsync()
        {
            return await _context.Price.ToListAsync();
        }

        public async Task<PairPrice> GetBySymbolAsync(string symbol)
        {
            return await _context.Price.FirstOrDefaultAsync(x => x.Symbol == symbol) ?? throw new Exception("Pair price not found");
        }
    }
}