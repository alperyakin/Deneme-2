using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Categories.Repositories;
using Deneme2.Services.ProductService.Domain.Products;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Repositories.Categories;
internal sealed class EfCategoryCommandRepository(
    ApplicationWriteDbContext context) : ICategoryCommandRepository
{
    public async Task<Result> DeleteProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        Product[] products = await context.Products
            .Where(p => p.Category == categoryId)
            .ToArrayAsync(cancellationToken);

        if (products.Length == 0)
            return Result.Success();

        foreach (Product item in products)
            item.Delete();

        context.Products.RemoveRange(products);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
