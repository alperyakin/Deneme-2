using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;
using Deneme2.Services.CategoryService.Domain.Categories.ReadModels;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.List;

internal sealed class GetCategoryListQueryHandler(
    ICategoryQueryRepository repository) : ICachedQueryHandler<GetCategoryListQuery, CategoryViewModel[]>
{
    public async Task<Result<CategoryViewModel[]>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        CategoryReadModel[] categories = await repository.GetCategories(cancellationToken);
        CategoryViewModel[] models = CategoryViewModel.Create(categories);
        return models;

    }
}
