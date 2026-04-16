using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Products.Fields;

namespace Deneme2.Services.ProductService.Domain.Categories.Repositories;
public interface ICategoryCommandRepository
{
    Task<Result> DeleteProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
}
