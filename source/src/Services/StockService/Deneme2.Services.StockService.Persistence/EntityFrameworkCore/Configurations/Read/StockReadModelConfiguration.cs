using Deneme2.Services.StockService.Domain.Stocks.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Configurations.Read;

public sealed class StockReadModelConfiguration : IEntityTypeConfiguration<StockReadModel>
{
    public void Configure(EntityTypeBuilder<StockReadModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
