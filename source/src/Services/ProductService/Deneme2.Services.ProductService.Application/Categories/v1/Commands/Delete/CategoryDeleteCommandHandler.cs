using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.BuildingBlocks.Caching.Base;
using Deneme2.Services.ProductService.Domain.Categories.Repositories;
using Deneme2.Services.ProductService.Domain.Products.Fields;

namespace Deneme2.Services.ProductService.Application.Categories.v1.Commands.Delete;

internal sealed class CategoryDeleteCommandHandler
    (ICategoryCommandRepository repository,
    ICacheService cacheService) : ICommandHandler<CategoryDeleteCommand>
{
    public async Task<Result> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
    {
        var categoryId = CategoryId.From(request.CategoryId);
        Result result = await repository.DeleteProductsByCategoryIdAsync(categoryId, cancellationToken);
        if (result.IsFailure)
            return result;

        string key = $"category:{request.CategoryId}:products";
        cacheService.Remove(key);

        return result;
    }
}
