using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.Parameters;

namespace Deneme2.Services.ProductService.Domain.Products.Repositories;
public interface IProductCommandRepository
{
    Task<Result<ProductId>> CreateProductAsync(ProductCreateParameters parameters, CancellationToken cancellationToken = default);
    Task<Result<ProductId>> CreateProductAsync(ProductCreateParameters parameters, IRuleBase<ProductCreateParameters> rule, CancellationToken cancellationToken = default);
    Task<Result> UpdateProductAsync(ProductUpdateParameters parameters, CancellationToken cancellationToken = default);
    Task<Result> UpdateProductAsync(ProductUpdateParameters parameters, IRuleBase<ProductUpdateParameters> rule, CancellationToken cancellationToken = default);
    Task<Result> DeleteProductAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<Result> MarkAsLowStockAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<Result> MarkAsInStockAsync(Guid productId, CancellationToken cancellationToken = default);
}
