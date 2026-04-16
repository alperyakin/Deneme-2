using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.ReadModels;
using Deneme2.Services.ProductService.Domain.Products.Repositories;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Repositories.Products;
internal sealed class EfProductQueryRepository(
    ApplicationReadDbContext context) : IProductQueryRepository
{
    public async Task<Maybe<ProductReadModel>> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .Where(product => product.Id == productId.Value)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<ProductReadModel[]> GetProductsByCategoryId(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        return context.Products
            .Where(product => product.Category == categoryId.Value)
            .OrderByDescending(product => product.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }

    public Task<ProductReadModel[]> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        return context.Products
            .OrderByDescending(product => product.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }
}
