using CSharpEssentials.EntityFrameworkCore;
using Deneme2.Services.CategoryService.Domain.Categories;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deneme2.Services.CategoryService.Persistence.EntityFrameworkCore.Configurations.Write;
internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.EntityBaseMap();

        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, id => CategoryId.From(id));

        builder
            .Property(p => p.Name)
            .HasConversion(name => name.Value, name => CategoryName.From(name))
            .HasMaxLength(CategoryName.MaxLength);

        builder.OptimisticConcurrencyVersionMap();

        builder.HasIndex(x => x.CreatedAt);
    }
}
