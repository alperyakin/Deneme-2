using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Exist;

internal sealed class CategoryExistQueryHandler(
    ICategoryQueryRepository repository) : IQueryHandler<CategoryExistQuery, bool>
{
    public async Task<Result<bool>> Handle(CategoryExistQuery request, CancellationToken cancellationToken)
    {
        bool isExist = await repository.ExistsAsync(CategoryId.From(request.CategoryId), cancellationToken);
        return Result.Success(isExist);
    }
}
