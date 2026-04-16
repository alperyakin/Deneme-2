using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Commands.Delete;

internal sealed class CategoryDeleteCommandHandler(
    ICategoryCommandRepository repository) : ICommandHandler<CategoryDeleteCommand>
{
    public Task<Result> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
    {
        var productId = CategoryId.From(request.CategoryId);
        return repository.DeleteCategoryAsync(productId, cancellationToken);
    }
}
