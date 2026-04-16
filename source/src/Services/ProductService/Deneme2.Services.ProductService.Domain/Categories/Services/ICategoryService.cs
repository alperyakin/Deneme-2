using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Products.Fields;

namespace Deneme2.Services.ProductService.Domain.Categories.Services;
public interface ICategoryService
{
    Task<Result<bool>> CategoryExistsAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
}
