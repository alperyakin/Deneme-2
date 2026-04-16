using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.Parameters;
using Deneme2.Services.CategoryService.Domain.Categories.Repositories;
namespace Deneme2.Services.CategoryService.Application.Categories.v1.Commands.Create;

internal sealed class CategoryCreateCommandHandler(
    ICategoryCommandRepository repository) : ICommandHandler<CategoryCreateCommand, CategoryId>
{
    public Task<Result<CategoryId>> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        CategoryCreateParameters parameters = request.ToParameters();
        return repository.CreateCategoryAsync(parameters, cancellationToken);
    }
}
