using CSharpEssentials;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.ReadModels;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;
using Deneme2.Services.CategoryService.Persistence.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.CategoryService.Persistence.EntityFrameworkCore.Repositories.Categories;
internal sealed class EfCategoryQueryRepository(
    ApplicationReadDbContext context) : ICategoryQueryRepository
{
    public Task<bool> ExistsAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        return context.Categories
            .AnyAsync(Category => Category.Id == categoryId.Value, cancellationToken);
    }

    public Task<CategoryReadModel[]> GetCategories(CancellationToken cancellationToken = default)
    {
        return context.Categories
            .OrderByDescending(Category => Category.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Maybe<CategoryReadModel>> GetCategoryByIdAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        return await context.Categories
            .Where(Category => Category.Id == categoryId.Value)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<CategoryReadModel[]> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        IQueryable<CategoryReadModel> query = context.Categories;

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(c => c.Name.Contains(name));

        return query
            .OrderByDescending(c => c.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }
}
