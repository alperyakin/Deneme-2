
using Deneme2.Services.ProductService.Domain.Products.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Configurations.Read;

public sealed class ProductReadModelConfiguration : IEntityTypeConfiguration<ProductReadModel>
{
    public void Configure(EntityTypeBuilder<ProductReadModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
