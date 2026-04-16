using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.ReadModels;

namespace Deneme2.Services.ProductService.Domain.Products.Repositories;
public interface IProductQueryRepository
{
    Task<Maybe<ProductReadModel>> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<ProductReadModel[]> GetProductsByCategoryId(CategoryId categoryId, CancellationToken cancellationToken = default);
    Task<ProductReadModel[]> GetAllProductsAsync(CancellationToken cancellationToken = default);
}
