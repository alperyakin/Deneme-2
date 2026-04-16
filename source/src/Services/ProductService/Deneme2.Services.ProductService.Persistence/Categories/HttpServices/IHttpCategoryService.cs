using Refit;

namespace Deneme2.Services.ProductService.Persistence.Categories.HttpServices;
public interface IHttpCategoryService
{
    [Get("/v1/exist/{categoryId}")]
    Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken = default);
}
