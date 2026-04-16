using CSharpEssentials;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.ReadModels;

namespace Deneme2.Services.CategoryService.Domain.Categories.Repositories;
public interface ICategoryQueryRepository
{
    Task<Maybe<CategoryReadModel>> GetCategoryByIdAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
    Task<CategoryReadModel[]> GetCategories(CancellationToken cancellationToken = default);
    Task<CategoryReadModel[]> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
}
