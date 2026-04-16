using CSharpEssentials;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Persistence.EntityFrameworkCore.Contexts;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;
using Deneme2.Services.CategoryService.Domain.Categories.Parameters;
using Deneme2.Services.CategoryService.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.CategoryService.Persistence.EntityFrameworkCore.Repositories.Categories;

internal sealed class EfCategoryCommandRepository(
    ApplicationWriteDbContext context) : ICategoryCommandRepository
{
    public async Task<Result<CategoryId>> CreateCategoryAsync(CategoryCreateParameters parameters, CancellationToken cancellationToken = default)
    {
        Result<Category> categoryResult = Category.Create(parameters);
        if (categoryResult.IsFailure)
            return categoryResult.Errors;

        Category category = categoryResult.Value;

        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }

    public async Task<Result> DeleteCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        Category? found = await context.Categories
            .Where(Category => Category.Id == categoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (found is null)
            return CategoryErrors.CategoryDoesNotExistError(categoryId);

        found.Delete();
        context.Categories.Remove(found);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
