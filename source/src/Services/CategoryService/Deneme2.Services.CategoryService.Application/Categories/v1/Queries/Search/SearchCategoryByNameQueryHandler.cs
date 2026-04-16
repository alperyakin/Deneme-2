using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;
using Deneme2.Services.CategoryService.Domain.Categories.ReadModels;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Search;

internal sealed class SearchCategoryByNameQueryHandler(
    ICategoryQueryRepository repository) : ICachedQueryHandler<SearchCategoryByNameQuery, CategoryViewModel[]>
{
    public async Task<Result<CategoryViewModel[]>> Handle(SearchCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        CategoryReadModel[] categories = await repository.SearchByNameAsync(request.Name ?? string.Empty, cancellationToken);
        CategoryViewModel[] models = CategoryViewModel.Create(categories);
        return models;
    }
}
