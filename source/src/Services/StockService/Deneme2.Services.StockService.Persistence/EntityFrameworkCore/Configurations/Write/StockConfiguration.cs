using CSharpEssentials.EntityFrameworkCore;
using Deneme2.Services.StockService.Domain.Stocks;
using Deneme2.Services.StockService.Domain.Stocks.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Configurations.Write;

internal sealed class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.EntityBaseMap();

        builder
            .Property(s => s.Id)
            .HasConversion(id => id.Value, id => StockId.From(id));

        builder
            .Property(s => s.ProductId)
            .IsRequired();

        builder
            .Property(s => s.Quantity)
            .IsRequired();

        builder.HasIndex(s => s.ProductId).IsUnique();

        builder.OptimisticConcurrencyVersionMap();
    }
}
