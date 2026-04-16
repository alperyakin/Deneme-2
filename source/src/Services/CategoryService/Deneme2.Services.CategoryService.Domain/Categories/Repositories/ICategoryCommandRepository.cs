using CSharpEssentials;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.Parameters;

namespace Deneme2.Services.CategoryService.Domain.Categories.Repositories;
public interface ICategoryCommandRepository
{
    Task<Result<CategoryId>> CreateCategoryAsync(CategoryCreateParameters parameters, CancellationToken cancellationToken = default);
    Task<Result> DeleteCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
}
