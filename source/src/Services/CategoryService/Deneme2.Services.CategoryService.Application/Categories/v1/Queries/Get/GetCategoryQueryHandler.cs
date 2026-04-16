using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;
using Deneme2.Services.CategoryService.Domain.Categories;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.ReadModels;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Get;

internal sealed class GetCategoryQueryHandler(
    ICategoryQueryRepository categoryQueryRepository) : ICachedQueryHandler<GetCategoryQuery, CategoryViewModel>
{
    public async Task<Result<CategoryViewModel>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryId = CategoryId.From(request.CategoryId);
        Maybe<CategoryReadModel> Category = await categoryQueryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);

        return Category.Match<Result<CategoryViewModel>>(
            value => CategoryViewModel.Create(value),
            () => CategoryErrors.CategoryDoesNotExistError(categoryId));
    }
}
