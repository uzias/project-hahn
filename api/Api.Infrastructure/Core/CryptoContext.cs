using Api.Domain.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Core;

public class CryptoContext(DbContextOptions<CryptoContext> options) : DbContext(options)
{
    public DbSet<PairPrice> Price { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PairPriceConfiguration());
    }

    class PairPriceConfiguration : IEntityTypeConfiguration<PairPrice>
    {
        public void Configure(EntityTypeBuilder<PairPrice> builder)
        {
            builder.HasKey(x => x.Symbol);
            builder.Property(x => x.Symbol).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Mins).IsRequired();
        }
    }
}